using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm
{
    public class SlackRtmClient : ISlackRtmClient
    {
        private const int PingTimeout = 5000;
        private const int BufferSize = 1024;

        private readonly ClientWebSocket clientWebSocket;
        private readonly IRtmRequestFactory rtmRequestFactory;
        private readonly IRtmResponseFactory rtmResponseFactory;
        private readonly ISlackRtmEventPublisher slackRtmEventPublisher;
        private readonly UTF8Encoding encoder;

        public SlackRtmClient(ClientWebSocket clientWebSocket,
                              IRtmRequestFactory rtmRequestFactory,
                              IRtmResponseFactory rtmResponseFactory,
                              ISlackRtmEventPublisher slackRtmEventPublisher)
        {
            this.clientWebSocket = clientWebSocket;
            this.rtmRequestFactory = rtmRequestFactory;
            this.rtmResponseFactory = rtmResponseFactory;
            this.slackRtmEventPublisher = slackRtmEventPublisher;

            slackRtmEventPublisher.AllMessages += (s, e) => Console.WriteLine($"<INFO> {e.Response}");

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
                    var json = await SendRequest();

                    try
                    {
                        var response = rtmResponseFactory.CreateResponse(json);
                        slackRtmEventPublisher.Fire(response);
                    }
                    catch (NotImplementedException e)
                    {
                        Console.WriteLine($"<ERROR> {e.Message}");
                    }
                }
            }

            async Task<string> SendRequest()
            {
                var buffer = new byte[BufferSize];
                var arraySegment = new ArraySegment<byte>(buffer);
                await clientWebSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                var json = encoder.GetString(buffer);

                return json;
            }
        }

        private void SetupPing()
        {
            Task.Factory.StartNew(Setup);

            async Task Setup()
            {
                while (clientWebSocket.State == WebSocketState.Open)
                {
                    await SendRequest();

                    await Task.Delay(PingTimeout);
                }
            }

            async Task SendRequest()
            {
                var pingRequest = rtmRequestFactory.CreatePingRequest();
                var buffer = Encoding.ASCII.GetBytes(pingRequest.ToJsonString());
                var message = new ArraySegment<byte>(buffer);
                await clientWebSocket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
