using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bashi.Core.Interface.Config.Group;
using Bashi.Core.Interface.Log;
using SlackApi.Core.Interface;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Client
{
    internal class SlackRtmClient : ISlackRtmClient
    {
        private const int BufferSize = 1024;

        private readonly ClientWebSocket clientWebSocket;
        private readonly int pingTimeout;
        private readonly IRtmRequestFactory rtmRequestFactory;
        private readonly IRtmResponseFactory rtmResponseFactory;
        private readonly ISlackRtmEventPublisher slackRtmEventPublisher;
        private readonly ISlackConnectionEventPublisher slackConnectionEventPublisher;
        private readonly IBashiLogger log;
        private readonly UTF8Encoding encoder;

        public SlackRtmClient(ClientWebSocket clientWebSocket,
                              ISlackConfigGroup slackConfigGroup,
                              IRtmRequestFactory rtmRequestFactory,
                              IRtmResponseFactory rtmResponseFactory,
                              ISlackRtmEventPublisher slackRtmEventPublisher,
                              ISlackConnectionEventPublisher slackConnectionEventPublisher,
                              IBashiLogger log)
        {
            this.clientWebSocket = clientWebSocket;
            pingTimeout = slackConfigGroup.PingTimeout;
            this.rtmRequestFactory = rtmRequestFactory;
            this.rtmResponseFactory = rtmResponseFactory;
            this.slackRtmEventPublisher = slackRtmEventPublisher;
            this.slackConnectionEventPublisher = slackConnectionEventPublisher;
            this.log = log;
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
                try
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
                            log.Error(e.Message);
                        }
                        finally
                        {
                            log.Debug(json);
                        }
                    }
                }
                catch (Exception ex)
                {
                    slackConnectionEventPublisher.RaiseRtmDisconnected();
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
                try
                {
                    while (clientWebSocket.State == WebSocketState.Open)
                    {
                        await SendRequest();

                        await Task.Delay(pingTimeout);
                    }
                }
                catch
                {
                    // Ignored
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
