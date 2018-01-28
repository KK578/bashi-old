using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SlackApi.Core.Interface.Rtm
{
    public interface IClientWebSocket
    {
        WebSocketState State { get; }

        Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> arraySegment, CancellationToken cancellationToken);

        Task SendAsync(ArraySegment<byte> buffer,
                       WebSocketMessageType messageType,
                       bool endOfMessage,
                       CancellationToken cancellationToken);

        Task ConnectAsync(Uri uri, CancellationToken cancellationToken);
    }
}
