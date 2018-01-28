using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Client
{
    internal class WebSocketManager : IWebSocketManager
    {
        private const int BufferSize = 1024;

        private readonly IClientWebSocket clientWebSocket;
        private readonly UTF8Encoding encoder;

        public WebSocketState State => clientWebSocket.State;

        public WebSocketManager(IClientWebSocket clientWebSocket)
        {
            this.clientWebSocket = clientWebSocket;
            this.encoder = new UTF8Encoding();
        }

        public async Task<string> ReceiveData()
        {
            var buffer = new byte[BufferSize];
            var arraySegment = new ArraySegment<byte>(buffer);

            await clientWebSocket.ReceiveAsync(arraySegment, CancellationToken.None);

            var result = encoder.GetString(buffer);

            return result;
        }

        public Task SendData(string data)
        {
            var buffer = Encoding.ASCII.GetBytes(data);
            var message = new ArraySegment<byte>(buffer);

            return clientWebSocket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public Task ConnectAsync(Uri uri)
        {
            return clientWebSocket.ConnectAsync(uri, CancellationToken.None);
        }
    }
}
