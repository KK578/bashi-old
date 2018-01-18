using System;
using Bashi.Interface.Connection;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Core.Interface.Web;

namespace Bashi.Slack.Connection
{
    public class SlackConnectionManager : IConnectionManager
    {
        private readonly ISlackWebClient slackWebClient;
        private readonly ISlackRtmClient slackRtmClient;

        public SlackConnectionManager(ISlackRtmClient slackRtmClient, ISlackWebClient slackWebClient)
        {
            this.slackRtmClient = slackRtmClient;
            this.slackWebClient = slackWebClient;
        }

        public void Connect(IConnectionParams details)
        {
            if (!(details is SlackConnectionParams slackDetails))
            {
                throw new ArgumentException($"Connection Params was not ${nameof(SlackConnectionParams)}",
                                            nameof(details));
            }

            SetupRtmClient(slackDetails.BotToken);
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
