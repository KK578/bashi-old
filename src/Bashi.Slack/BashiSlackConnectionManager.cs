using System;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi.Slack
{
    public class BashiSlackConnectionManager
    {
        private readonly SlackWebClient slackWebClient;
        private readonly SlackRtmClient slackRtmClient;

        public BashiSlackConnectionManager(SlackRtmClient slackRtmClient, SlackWebClient slackWebClient)
        {
            this.slackRtmClient = slackRtmClient;
            this.slackWebClient = slackWebClient;
        }

        public void Connect(string token)
        {
            SetupRtmClient(token);
        }

        private async void SetupRtmClient(string token)
        {
            var connectResponse = await slackWebClient.RtmConnectAsync(token);
            await slackRtmClient.ConnectAsync(connectResponse.WebSocketUrl);

            Console.WriteLine("Connected.");
        }
    }
}
