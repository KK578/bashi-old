using System;
using Bashi.Interface.Connection;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi.Slack.Connection
{
    public class SlackConnectionManager : IConnectionManager
    {
        private readonly SlackWebClient slackWebClient;
        private readonly SlackRtmClient slackRtmClient;

        public SlackConnectionManager(SlackRtmClient slackRtmClient, SlackWebClient slackWebClient)
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
