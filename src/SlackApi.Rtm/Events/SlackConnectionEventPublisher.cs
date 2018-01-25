using System;
using SlackApi.Core.Interface;

namespace SlackApi.Rtm.Events
{
    internal class SlackConnectionEventPublisher : ISlackConnectionEventPublisher
    {
        public event EventHandler<EventArgs> RtmDisconnected;

        public void RaiseRtmDisconnected()
        {
            RtmDisconnected?.Invoke(this, EventArgs.Empty);
        }
    }
}
