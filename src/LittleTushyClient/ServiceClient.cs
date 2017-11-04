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

        private const int MAX_CLIENTS = 100;
        private ClientWebSocket[] clients = new ClientWebSocket[MAX_CLIENTS];
        private int[] clientStatus = new int[MAX_CLIENTS];
        
        private readonly Uri baseUrl;

        public ServiceClient(Uri baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<TResult> RequestAsync<TResult, TRequest>(
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
                    mem.Seek(0, SeekOrigin.Begin);
                    
                    int read;
                    do
                    {
                        if ((read = mem.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            await socket.SendAsync(
                                new ArraySegment<byte>(buffer),
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

                    var result = Serializer.Deserialize<TResult>(mem);

                    return result;
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
