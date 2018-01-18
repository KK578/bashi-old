using System;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Core.Interface.Web;

namespace Bashi
{
    internal class BashiApp
    {
        private readonly ISlackWebClient slackWebClient;
        private readonly ISlackRtmClient slackRtmClient;

        public BashiApp(ISlackRtmClient slackRtmClient, ISlackWebClient slackWebClient)
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
