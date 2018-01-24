using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Bashi.Core.Interface.Config.Group;
using Bashi.Core.Interface.Log;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Client
{
    internal class SlackRtmClient : ISlackRtmClient
    {
        private readonly int pingTimeout;
        private readonly IWebSocketManager webSocketManager;
        private readonly IRtmRequestFactory rtmRequestFactory;
        private readonly IRtmResponseFactory rtmResponseFactory;
        private readonly ISlackRtmEventPublisher slackRtmEventPublisher;
        private readonly IBashiLogger log;

        public SlackRtmClient(IWebSocketManager webSocketManager,
                              ISlackConfigGroup slackConfigGroup,
                              IRtmRequestFactory rtmRequestFactory,
                              IRtmResponseFactory rtmResponseFactory,
                              ISlackRtmEventPublisher slackRtmEventPublisher,
                              IBashiLogger log)
        {
            pingTimeout = slackConfigGroup.PingTimeout;
            this.webSocketManager = webSocketManager;
            this.rtmRequestFactory = rtmRequestFactory;
            this.rtmResponseFactory = rtmResponseFactory;
            this.slackRtmEventPublisher = slackRtmEventPublisher;
            this.log = log;
        }

        public async Task ConnectAsync(string url)
        {
            await webSocketManager.ConnectAsync(new Uri(url));

            SetupReceiver();
            SetupPing();
        }

        private void SetupReceiver()
        {
            Task.Factory.StartNew(Setup);

            async void Setup()
            {
                while (webSocketManager.State == WebSocketState.Open)
                {
                    var json = await webSocketManager.ReceiveData();

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
        }

        private void SetupPing()
        {
            Task.Factory.StartNew(Setup);

            async Task Setup()
            {
                while (webSocketManager.State == WebSocketState.Open)
                {
                    var pingRequest = rtmRequestFactory.CreatePingRequest();

                    await webSocketManager.SendData(pingRequest.ToJsonString());
                    await Task.Delay(pingTimeout);
                }
            }
        }
    }
}
