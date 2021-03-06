using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LittleTushy.Server.LZ4;
using ProtoBuf;

namespace LittleTushy.Server
{
    /// <summary>
    /// Handles requests from the LittleTushyServer.
    /// Uses a service map to locate controllers and actions to handle requests.
    /// </summary>
    public class LittleTushyServer {
        private readonly ServiceMap serviceMap;
        private readonly IServiceProvider serviceProvider;

        internal readonly ActionResult ServiceNotFound = new ActionResult { StatusCode = StatusCode.NotFound, Message = "Controller or Action was not found", Contents = null };

        /// <param name="serviceMap">The ServiceMap configured at startup the contains the registered controllers and action methods</param>
        /// <param name="serviceProvider">The service provider LittleTushy was added to that can be used to resolve ServiceController isntances with at runtime</param>
        public LittleTushyServer (
            ServiceMap serviceMap,
            IServiceProvider serviceProvider
        ) {
            this.serviceMap = serviceMap;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Start handling requests and replies from the connected web socket
        /// </summary>
        public async Task HandleClientAsync (WebSocket webSocket) {

            var receiveBuffer = new byte[4096];
            var sendBuffer = new byte[4096];

            using (var mem = new MemoryStream())
            {

                while (webSocket.State == WebSocketState.Open)
                {

                    mem.SetLength(0);
                    mem.Seek(0, SeekOrigin.Begin);
                    WebSocketReceiveResult socketReceive = null;

                    do
                    {
                        var receiveSegment = new ArraySegment<byte>(receiveBuffer);
                        socketReceive = await webSocket.ReceiveAsync(receiveSegment, CancellationToken.None);
                        if (socketReceive.MessageType == WebSocketMessageType.Close)
                        {
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                            return;
                        }
                        mem.Write(receiveSegment.Array, receiveSegment.Offset, socketReceive.Count);
                    }
                    while (!socketReceive.EndOfMessage);

                    mem.Seek(0, SeekOrigin.Begin);

                    var request = Serializer.Deserialize<ActionRequest>(mem);

                    var result = await HandleRequestAsync(request);

                    mem.SetLength(0);
                    mem.Seek(0, SeekOrigin.Begin);

                    Serializer.Serialize(mem, result);

                    mem.Seek(0, SeekOrigin.Begin);

                    int read = 0;
                    while ((read = mem.Read(sendBuffer, 0, sendBuffer.Length)) > 0)
                    {
                        var endOfMessage = read < sendBuffer.Length;
                        var sendSegment = new ArraySegment<byte>(sendBuffer, 0, read);
                        await webSocket.SendAsync(sendSegment, WebSocketMessageType.Binary, endOfMessage, CancellationToken.None);
                    }
                }

            }

        }

        /// <summary>
        /// Handles generating a response to a request sent to a request-response controller action on the server
        /// </summary>
        public async Task<ActionResult> HandleRequestAsync (ActionRequest request) {
            var action = serviceMap.GetActionDefintion (request.Controller, request.Action);

            if (action == null) {
                return new ActionResult {
                StatusCode = StatusCode.ActionNotFound,
                Message = $"Could not find service {request.Controller}/{request.Action}"
                };
            }

            var controllerInstance = serviceProvider.GetService (action.ControllerType) as ServiceController;

            var actionResult = await action.InvokeFunction (controllerInstance, request);

            if (action.Compress)
            {
                actionResult.Contents = LZ4Codec.Wrap(actionResult.Contents);
                actionResult.IsCompressed = true;                
            }

            return actionResult;

        }

    }
}