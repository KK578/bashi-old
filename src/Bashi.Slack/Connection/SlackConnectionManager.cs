using System;
using System.Threading.Tasks;
using Bashi.Core.Interface.Config.Group;
using Bashi.Core.Interface.Connection;
using Bashi.Core.Interface.Log;
using SlackApi.Core.Interface;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Core.Interface.Web;

namespace Bashi.Slack.Connection
{
    internal class SlackConnectionManager : ISlackConnectionManager
    {
        private readonly string botToken;
        private readonly ISlackRtmClient slackRtmClient;
        private readonly ISlackWebClient slackWebClient;
        private readonly IBashiLogger log;

        public SlackConnectionManager(ISlackConfigGroup slackConfigGroup,
                                      ISlackRtmClient slackRtmClient,
                                      ISlackWebClient slackWebClient,
                                      ISlackConnectionEventPublisher slackConnectionEventPublisher,
                                      IBashiLogger log)
        {
            botToken = slackConfigGroup.BotToken;
            this.slackRtmClient = slackRtmClient;
            this.slackWebClient = slackWebClient;
            this.log = log;

            slackConnectionEventPublisher.RtmDisconnected += async (s, e) => await ConnectAsync();
        }

        public Task ConnectAsync()
        {
            return SetupRtmClient();
        }

        private async Task SetupRtmClient()
        {
            var connectResponse = await slackWebClient.RtmConnectAsync(botToken);

            if (string.IsNullOrEmpty(connectResponse.WebSocketUrl))
            {
                throw new Exception("No Websocket Url from RtmConnect.");
            }

            await slackRtmClient.ConnectAsync(connectResponse.WebSocketUrl);

            log.Info("Connected to RTM API.");
        }
    }
}
