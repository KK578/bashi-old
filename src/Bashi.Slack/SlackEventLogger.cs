using System;
using Bashi.Core.Interface.Logger;
using SlackApi.Core.Interface.Rtm;

namespace Bashi.Slack
{
    public class SlackEventLogger : IEventLogger
    {
        private readonly ISlackRtmEventPublisher slackRtmEventPublisher;

        public SlackEventLogger(ISlackRtmEventPublisher slackRtmEventPublisher)
        {
            this.slackRtmEventPublisher = slackRtmEventPublisher;
        }

        public void AttachLogger()
        {
            slackRtmEventPublisher.AllMessages += (s, e) => Console.WriteLine($"<INFO> {e.Response}");
        }
    }
}
