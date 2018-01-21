using Bashi.Core.Interface.Log;
using SlackApi.Core.Interface.Rtm;

namespace Bashi.Slack
{
    public class SlackEventLogger : IEventLogger
    {
        private readonly ISlackRtmEventPublisher slackRtmEventPublisher;
        private readonly IBashiLogger log;

        public SlackEventLogger(ISlackRtmEventPublisher slackRtmEventPublisher, IBashiLogger log)
        {
            this.slackRtmEventPublisher = slackRtmEventPublisher;
            this.log = log;
        }

        public void AttachLogger()
        {
            slackRtmEventPublisher.AllMessages += (s, e) => log.Info(e.Response.ToString());
        }
    }
}
