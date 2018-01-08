using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Api.Rtm
{
    public class SlackRtmClient
    {
        private readonly ClientWebSocket webSocket;
        private int messageId;
        private const int PingTimeout = 5000;

        public SlackRtmClient()
        {
            webSocket = new ClientWebSocket();
        }

        public async Task ConnectAsync(string url)
        {
            await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);

            SetupReceiver();
            SetupPing();
        }

        private void SetupReceiver()
        {
            var encoder = new UTF8Encoding();

            Task.Factory.StartNew(async () =>
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    var buffer = new byte[1024];
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    Console.WriteLine($"[{result.Count}] {encoder.GetString(buffer)}");
                }
            });
        }
        
        private void SetupPing()
        {
            Task.Factory.StartNew(async () =>
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    await Task.Delay(PingTimeout);

                    var buffer = Encoding.ASCII.GetBytes($"{{\"id\": \"{messageId++}\", \"type\": \"ping\"}}");
                    var message = new ArraySegment<byte>(buffer);

                    await webSocket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            });
        }
    }
}
