using System;
using SlackApi.Rtm;
using SlackApi.Web;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly SlackWebClient slackWebClient;
        private readonly SlackRtmClient slackRtmClient;

        public BashiApp(SlackRtmClient slackRtmClient, SlackWebClient slackWebClient)
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
