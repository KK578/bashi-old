using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Slack.Api.Message.Request.Rtm;

namespace Slack.Api.Rtm
{
    public class SlackRtmClient
    {
        private const int PingTimeout = 5000;
        private const int BufferSize = 1024;
        
        private readonly ClientWebSocket webSocket;
        private int messageId;
        private readonly UTF8Encoding encoder = new UTF8Encoding();

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
            Task.Factory.StartNew(Setup);

            async void Setup()
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    var buffer = new byte[BufferSize];
                    var arraySegment = new ArraySegment<byte>(buffer);
                    var result = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);

                    Console.WriteLine($"[{result.Count}] {encoder.GetString(buffer)}");
                }
            }
        }

        private void SetupPing()
        {
            Task.Factory.StartNew(Setup);
            
            async Task Setup()
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    var pingRequest = new PingRequest(messageId++);
                    var buffer = Encoding.ASCII.GetBytes(pingRequest.ToJsonString());
                    var message = new ArraySegment<byte>(buffer);

                    await webSocket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
                    await Task.Delay(PingTimeout);
                }
            }
        }
    }
}
