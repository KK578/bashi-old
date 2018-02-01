using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SlackApi.Core.Interface.Rtm;

namespace Bashi.Test.Util.Mock.SlackApi
{
    public class MockClientWebSocket : BashiMock, IClientWebSocket
    {
        public byte[] TestByteArray { get; set; } = { };
        public string LastMessage { get; private set; }

        public WebSocketState State
        {
            get
            {
                IncrementCallCount();
                return WebSocketState.Closed;
            }
        }

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
            LastMessage = string.Join("", Encoding.ASCII.GetChars(buffer.Array));

            return Task.Delay(0);
        }

        public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            IncrementCallCount();

            return Task.Delay(0);
        }
    }
}
