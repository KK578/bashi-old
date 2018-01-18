using SlackApi.Core.Data.Message.Rtm.Request;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Core.Factory.Message
{
    public class RtmRequestFactory : IRtmRequestFactory
    {
        private int pingMessageId;

        public PingRequest CreatePingRequest() => new PingRequest(pingMessageId++);
    }
}
