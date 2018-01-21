using System;
using Bashi.Core.Interface.Connection;
using SlackApi.Core.Interface.Rtm;
using SlackApi.Core.Interface.Web;

namespace Bashi.Slack.Connection
{
    public class SlackConnectionManager : ISlackConnectionManager
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
            if (!(details is ISlackConnectionParams slackDetails))
            {
                throw new ArgumentException($"Connection Params was not ${nameof(ISlackConnectionParams)}",
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
