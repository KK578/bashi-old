using System;
using SlackApi.Core.Base.Rtm;

namespace SlackApi.Core.Events.Rtm
{
    public class RtmMessageEventArgs<T> : EventArgs
        where T : class
    {
        public T Response { get; }

        public RtmMessageEventArgs(BaseRtmResponse response)
        {
            Response = response as T;
        }
    }
}
