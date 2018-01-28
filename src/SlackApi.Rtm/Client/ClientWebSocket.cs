using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Client
{
    internal class ClientWebSocket : IClientWebSocket
    {
        private readonly System.Net.WebSockets.ClientWebSocket underlyingWebSocket;

        public WebSocketState State => underlyingWebSocket.State;

        public ClientWebSocket()
        {
            underlyingWebSocket = new System.Net.WebSockets.ClientWebSocket();
        }

        public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> arraySegment,
                                                         CancellationToken cancellationToken) =>
            underlyingWebSocket.ReceiveAsync(arraySegment, cancellationToken);

        public Task SendAsync(ArraySegment<byte> buffer,
                             WebSocketMessageType messageType,
                             bool endOfMessage,
                             CancellationToken cancellationToken) =>
            underlyingWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);

        public Task ConnectAsync(Uri uri, CancellationToken cancellationToken) =>
            underlyingWebSocket.ConnectAsync(uri, cancellationToken);
    }
}
