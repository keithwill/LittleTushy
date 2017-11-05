using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using ProtoBuf;

namespace LittleTushyClient
{
    public class ServiceClient : IDisposable
    {

        private const int MAX_CLIENTS = 1000;
        private ClientWebSocket[] clients = new ClientWebSocket[MAX_CLIENTS];
        private int[] clientStatus = new int[MAX_CLIENTS];
        
        private readonly Uri baseUrl;

        public ServiceClient(Uri baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<ActionResult<TResult>> RequestAsync<TResult, TRequest>(
            string controllerName,
            string actionName,
            TRequest request
        )
        {
            var socket = await GetWebSocket();

            try
            {
                var buffer = new byte[4096];
                using (var mem = new MemoryStream())
                {
                    
                    Serializer.Serialize(mem, request);

                    var requestContentsBytes = mem.ToArray();
                    
                    mem.SetLength(0);
                    mem.Seek(0, SeekOrigin.Begin);

                    var actionRequest = new ActionRequest
                    {
                        Action = actionName,
                        Controller = controllerName,
                        Contents = requestContentsBytes
                    };

                    Serializer.Serialize(mem, actionRequest);
                    mem.Seek(0, SeekOrigin.Begin);

                    int read;
                    do
                    {

                        int readLength = mem.Length > buffer.Length ? buffer.Length : (int)mem.Length;

                        if ((read = mem.Read(buffer, 0, readLength)) > 0)
                        {
                            await socket.SendAsync(
                                new ArraySegment<byte>(buffer, 0, readLength),
                                WebSocketMessageType.Binary,
                                !(read == buffer.Length),
                                CancellationToken.None
                            );
                        }

                    } while(read == buffer.Length);
                    
                    mem.SetLength(0);
                    mem.Seek(0, SeekOrigin.Begin);

                    WebSocketReceiveResult recieveResult;
                    do
                    {
                        var receiveSegment = new ArraySegment<byte>(buffer);
                        recieveResult = await socket.ReceiveAsync(receiveSegment, CancellationToken.None);

                        if (recieveResult.MessageType == WebSocketMessageType.Close)
                        {
                            await socket.CloseAsync(
                                WebSocketCloseStatus.NormalClosure,
                                "",
                                CancellationToken.None
                            );
                        }

                        mem.Write (receiveSegment.Array, receiveSegment.Offset, recieveResult.Count);

                    } while (!recieveResult.EndOfMessage);

                    mem.Seek(0, SeekOrigin.Begin);

                    var resultAction = Serializer.Deserialize<ActionResult<TResult>>(mem);

                    mem.SetLength(0);
                    mem.Seek(0, SeekOrigin.Begin);

                    mem.Write(resultAction.Contents, 0, resultAction.Contents.Length);
                    mem.Seek(0, SeekOrigin.Begin);

                    resultAction.Result = Serializer.Deserialize<TResult>(mem);

                    return resultAction;
                }
            }
            finally
            {
                ReturnWebSocket(socket);
            }

            
        }

        public void ReturnWebSocket(ClientWebSocket socket)
        {
            for(int i = 0; i < clients.Length; i++)
            {
                if (clients[i] == socket)
                {
                    Interlocked.CompareExchange(ref clientStatus[i], 0, 1);
                }
            }
        }

        public async Task<ClientWebSocket> GetWebSocket()
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
