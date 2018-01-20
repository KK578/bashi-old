using System;
using SlackApi.Core.Interface.Rtm;

namespace Bashi.Slack
{
    public class BashiRtmLogger
    {
        private readonly ISlackRtmEventPublisher slackRtmEventPublisher;

        public BashiRtmLogger(ISlackRtmEventPublisher slackRtmEventPublisher)
        {
            this.slackRtmEventPublisher = slackRtmEventPublisher;
        }

        public void AttachLogger()
        {
            slackRtmEventPublisher.AllMessages += (s, e) => Console.WriteLine($"<INFO> {e.Response}");
        }
    }
}
