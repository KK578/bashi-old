using System;

namespace SlackApi.Core.Interface
{
    public interface ISlackConnectionEventPublisher
    {
        event EventHandler<EventArgs> RtmDisconnected;

        void RaiseRtmDisconnected();
    }
}
