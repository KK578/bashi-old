using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SlackApi.Core.Data.Message.Rtm.Request;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm
{
    public class SlackRtmClient
    {
        private const int PingTimeout = 5000;
        private const int BufferSize = 1024;

        private readonly ClientWebSocket clientWebSocket;
        private readonly UTF8Encoding encoder;
        private readonly ISocketDecoder socketDecoder;
        private readonly IRtmResponseFactory rtmResponseFactory;

        private int messageId;

        public SlackRtmClient(ClientWebSocket clientWebSocket,
                              ISocketDecoder socketDecoder,
                              IRtmResponseFactory rtmResponseFactory)
        {
            this.clientWebSocket = clientWebSocket;
            this.socketDecoder = socketDecoder;
            this.rtmResponseFactory = rtmResponseFactory;

            encoder = new UTF8Encoding();
        }

        public async Task ConnectAsync(string url)
        {
            await clientWebSocket.ConnectAsync(new Uri(url), CancellationToken.None);

            SetupReceiver();
            SetupPing();
        }

        private void SetupReceiver()
        {
            Task.Factory.StartNew(Setup);

            async void Setup()
            {
                while (clientWebSocket.State == WebSocketState.Open)
                {
                    var buffer = new byte[BufferSize];
                    var arraySegment = new ArraySegment<byte>(buffer);
                    await clientWebSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                    var json = encoder.GetString(buffer);

                    try
                    {
                        var response = rtmResponseFactory.CreateResponse(json);
                        Console.WriteLine($"<INFO> {response}");
                    }
                    catch (NotImplementedException e)
                    {
                        Console.WriteLine($"<ERROR> {e.Message}");
                    }
                    finally
                    {
                        Console.WriteLine($"<DEBUG> {json}");
                    }
                }
            }
        }

        private void SetupPing()
        {
            Task.Factory.StartNew(Setup);

            async Task Setup()
            {
                while (clientWebSocket.State == WebSocketState.Open)
                {
                    var pingRequest = new PingRequest(messageId++);
                    var buffer = Encoding.ASCII.GetBytes(pingRequest.ToJsonString());
                    var message = new ArraySegment<byte>(buffer);

                    await clientWebSocket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
                    await Task.Delay(PingTimeout);
                }
            }
        }
    }
}
