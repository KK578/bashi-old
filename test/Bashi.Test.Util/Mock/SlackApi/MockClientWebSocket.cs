using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using SlackApi.Core.Interface.Rtm;

namespace Bashi.Test.Util.Mock.SlackApi
{
    public class MockClientWebSocket : BashiMock, IClientWebSocket
    {
        public byte[] TestByteArray { get; set; } = { };

        public WebSocketState State { get; }

        public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> arraySegment,
                                                         CancellationToken cancellationToken)
        {
            IncrementCallCount();
            var array = arraySegment.Array;

            for (var i = 0; i < TestByteArray.Length; i++)
            {
                array[i] = TestByteArray[i];
            }

            return Task.FromResult<WebSocketReceiveResult>(null);
        }

        public Task SendAsync(ArraySegment<byte> buffer,
                              WebSocketMessageType messageType,
                              bool endOfMessage,
                              CancellationToken cancellationToken)
        {
            IncrementCallCount();

            return Task.Delay(0);
        }

        public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            IncrementCallCount();

            return Task.Delay(0);
        }
    }
}
