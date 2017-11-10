using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using LittleTushy.Client.LZ4;
using ProtoBuf;

namespace LittleTushy.Client
{
    /// <summary>
    /// A client for calling Little Tushy services.
    /// Like HttpClient, this client should only be instantiated one time per remote host
    /// and the instance shared. This can normally be done by storing the instance in IoC, or 
    /// putting it into a static variable
    /// </summary>
    ///<remarks>
    /// WebSockets are can only handle a single request and reply at a time. This client
    /// maintains a pool of WebSocket connections to the remote host and will use the first
    /// free client in the pool, if available, or create a new connection and add it to the pool
    /// up to a maximum pool size. If the pool is full up to the maximum connection size, this 
    /// client will wait for a connection in the pool to become available.
    /// </remarks>
    public class ServiceClient : IDisposable
    {

        private const int MAX_CLIENTS = 1000;
        private ClientWebSocket[] clients = new ClientWebSocket[MAX_CLIENTS];
        private int[] clientStatus = new int[MAX_CLIENTS];

        private const string CONNECT_PATH = "lt";
        
        private readonly Uri baseUrl;

        /// <summary>
        /// Instantiates a service client for calling Little Tushy services. Instances should be
        /// shared per remote host. Instantiating a client for each request may result in poor performance
        /// </summary>
        /// <param name="hostname">The hostname of the remote server to connect to</param>
        /// <param name="port">The port to connect to the remote server on</param>
        /// <param name="useSecure">If the connection should use the secure websocket protocol or not (i.e. wss or ws)</param>
        public ServiceClient(string hostname, int port, bool useSecure = true)
        {
            
            var scheme = "ws" + (useSecure ? "s" : "");

            this.baseUrl = new Uri($"{scheme}://{hostname}:{port}/{CONNECT_PATH}");
        }

        /// <summary>
        /// Pass a parameter to a remote controller action and get a task to await the result
        /// </summary>
        /// <param name="controllerName">The name of the controller on the remote host</param>
        /// <param name="actionName">The name of the action on the remote host</param>
        /// <param name="request">The parameter to pass to the controller action</param>
        /// <param name="cancellationToken">A token to signal canceling the request</param>
        /// <returns>A task to await the result of the remote call</returns>
        public async Task<ActionResult<TResult>> RequestAsync<TResult, TRequest>(
            string controllerName,
            string actionName,
            TRequest request,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, request);
                var requestBytes = stream.ToArray();
                return await RequestWithContents<TResult>(controllerName, actionName, requestBytes, stream, cancellationToken);
            }
        }

        /// <summary>
        /// Make a parameterless request to a remote controller action and get a task to await the result
        /// </summary>
        /// <param name="controllerName">The name of the controller on the remote host</param>
        /// <param name="actionName">The name of the action on the remote host</param>
        /// <param name="cancellationToken">A token to signal canceling the request</param>
        /// <returns>A task to await the result of the remote call</returns>
        public async Task<ActionResult<TResult>> RequestAsync<TResult>(
            string controllerName,
            string actionName,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            using (var stream = new MemoryStream())
            {
                return await RequestWithContents<TResult>(controllerName, actionName, null, stream, cancellationToken);
            }
        }

        /// <summary>
        /// Internal method with the code common to requesting from a controller action whether or not it
        /// has serialized parameter content.
        /// </summary>
        private async Task<ActionResult<TResult>> RequestWithContents<TResult>(
            string controllerName,
            string actionName,
            byte[] requestBytes,
            MemoryStream stream,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            var socket = await GetWebSocket();

            try
            {
                var buffer = new byte[4096];
                    
                stream.SetLength(0);
                stream.Seek(0, SeekOrigin.Begin);

                var actionRequest = new ActionRequest
                {
                    Action = actionName,
                    Controller = controllerName,
                    Contents = requestBytes
                };

                Serializer.Serialize(stream, actionRequest);
                stream.Seek(0, SeekOrigin.Begin);

                int read;
                do
                {

                    int readLength = stream.Length > buffer.Length ? buffer.Length : (int)stream.Length;

                    if ((read = stream.Read(buffer, 0, readLength)) > 0)
                    {
                        await socket.SendAsync(
                            new ArraySegment<byte>(buffer, 0, readLength),
                            WebSocketMessageType.Binary,
                            !(read == buffer.Length),
                            cancellationToken
                        );
                    }

                } while(read == buffer.Length);
                
                stream.SetLength(0);
                stream.Seek(0, SeekOrigin.Begin);

                WebSocketReceiveResult recieveResult;
                do
                {
                    var receiveSegment = new ArraySegment<byte>(buffer);
                    recieveResult = await socket.ReceiveAsync(receiveSegment, cancellationToken);

                    if (recieveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            "",
                            cancellationToken
                        );
                    }

                    stream.Write (receiveSegment.Array, receiveSegment.Offset, recieveResult.Count);

                } while (!recieveResult.EndOfMessage);

                stream.Seek(0, SeekOrigin.Begin);

                var resultAction = Serializer.Deserialize<ActionResult<TResult>>(stream);

                if (resultAction.IsCompressed)
                {
                    resultAction.Contents = LZ4Codec.Unwrap(resultAction.Contents);
                }

                stream.SetLength(0);
                stream.Seek(0, SeekOrigin.Begin);

                stream.Write(resultAction.Contents, 0, resultAction.Contents.Length);
                stream.Seek(0, SeekOrigin.Begin);

                resultAction.Result = Serializer.Deserialize<TResult>(stream);

                return resultAction;
            }
            finally
            {
                ReturnWebSocket(socket);
            }

            
        }

        /// <summary>
        /// Return a socket to the pool by signalling on its index
        /// </summary>
        /// <param name="socket">The socket to return</param>
        private void ReturnWebSocket(ClientWebSocket socket)
        {
            for(int i = 0; i < clients.Length; i++)
            {
                if (clients[i] == socket)
                {
                    Interlocked.CompareExchange(ref clientStatus[i], 0, 1);
                }
            }
        }

        /// <summary>
        /// Grab a socket from the pool if one is free, otherwise create a new one,
        /// unless the pool is full, then wait until one is free
        /// </summary>
        private async Task<ClientWebSocket> GetWebSocket()
        {

            do 
            {
                for(int i = 0; i < clients.Length; i++)
                {
                    if (Interlocked.CompareExchange(ref clientStatus[i], 1, 0) == 0)
                    {
                        ClientWebSocket client = clients[i];
                        if (client == null)
                        {
                            client = new ClientWebSocket();
                            clients[i] = client;
                            await client.ConnectAsync(baseUrl, CancellationToken.None);                        
                        }
                        else if (client.State != WebSocketState.Open)
                        {
                            client.Dispose();
                            client = new ClientWebSocket();
                            clients[i] = client;
                            await client.ConnectAsync(baseUrl, CancellationToken.None); 
                        }
                        return client;
                    }
                }
                //We went through the whole list and all of the clients in the pool
                //were busy. Wait a ms, and try again.
                await Task.Delay(1);
            } while(true);



        }

        public void Dispose()
        {
            //ToDo: Implement the full dispose pattern
            //ToDo: Fire off cancellation tokens (not yet implemented)
            //ToDo: Wait for a linger time period?

            for(int i = 0; i < clients.Length; i++)
            {

                try
                {
                    clients[i]?.Dispose();
                }
                catch(Exception)
                {
                    
                }

            }

        }

    }
}
