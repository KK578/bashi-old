using System;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi
{
    internal class BashiApp
    {
        private SlackWebClient slackWebClient;
        private SlackRtmClient slackRtmClient;
        private readonly string token;

        public BashiApp(string token)
        {
            this.token = token;
        }

        public void Connect()
        {
            SetupWebClient();
        }

        private async void SetupWebClient()
        {
            slackWebClient = new SlackWebClient();
            var connectResponse = await slackWebClient.RtmConnectAsync(token);

            SetupRtmClient(connectResponse.WebSocketUrl);
        }

        private async void SetupRtmClient(string url)
        {
            slackRtmClient = new SlackRtmClient();
            await slackRtmClient.ConnectAsync(url);

            Console.WriteLine("Connected.");
        }
    }
}
