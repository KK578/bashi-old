using System;
using Bashi.Core.Interface.Config;
using Bashi.Core.Interface.Config.Group;
using Bashi.Core.Interface.Connection;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Core.Interface.Web;

namespace Bashi.Slack.Connection
{
    public class SlackConnectionManager : ISlackConnectionManager
    {
        private readonly ISlackConfigGroup slackConfigGroup;
        private readonly ISlackWebClient slackWebClient;
        private readonly ISlackRtmClient slackRtmClient;

        public SlackConnectionManager(ISlackConfigGroup slackConfigGroup,
                                      ISlackRtmClient slackRtmClient,
                                      ISlackWebClient slackWebClient)
        {
            this.slackConfigGroup = slackConfigGroup;
            this.slackRtmClient = slackRtmClient;
            this.slackWebClient = slackWebClient;
        }

        public void Connect()
        {
            SetupRtmClient(slackConfigGroup.BotToken);
        }

        private async void SetupRtmClient(string token)
        {
            var connectResponse = await slackWebClient.RtmConnectAsync(token);

            if (string.IsNullOrEmpty(connectResponse.WebSocketUrl))
            {
                throw new Exception("No Websocket Url from RtmConnect.");
            }

            await slackRtmClient.ConnectAsync(connectResponse.WebSocketUrl);

            Console.WriteLine("Connected.");
        }
    }
}
