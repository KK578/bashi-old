using System;
using Autofac;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi
{
    internal class BashiApp
    {
        private SlackWebClient slackWebClient;
        private SlackRtmClient slackRtmClient;
        private readonly string token;
        private readonly IContainer container;

        public BashiApp(string token, IContainer container)
        {
            this.token = token;
            this.container = container;
        }

        public void Connect()
        {
            SetupWebClient();
        }

        private async void SetupWebClient()
        {
            slackWebClient = container.Resolve<SlackWebClient>();
            var connectResponse = await slackWebClient.RtmConnectAsync(token);

            SetupRtmClient(connectResponse.WebSocketUrl);
        }

        private async void SetupRtmClient(string url)
        {
            slackRtmClient = container.Resolve<SlackRtmClient>();
            await slackRtmClient.ConnectAsync(url);

            Console.WriteLine("Connected.");
        }
    }
}
