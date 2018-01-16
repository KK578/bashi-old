﻿using System;
using Bashi.Core;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi.Slack
{
    public class BashiSlackConnectionManager : IBashiConnectionManager
    {
        private readonly SlackWebClient slackWebClient;
        private readonly SlackRtmClient slackRtmClient;

        public BashiSlackConnectionManager(SlackRtmClient slackRtmClient, SlackWebClient slackWebClient)
        {
            this.slackRtmClient = slackRtmClient;
            this.slackWebClient = slackWebClient;
        }

        public void Connect(IConnectionParams details)
        {
            if (details is SlackConnectionParams slackDetails)
            {
                SetupRtmClient(slackDetails.BotToken);
            }
        }

        private async void SetupRtmClient(string token)
        {
            var connectResponse = await slackWebClient.RtmConnectAsync(token);
            await slackRtmClient.ConnectAsync(connectResponse.WebSocketUrl);

            Console.WriteLine("Connected.");
        }
    }
}
