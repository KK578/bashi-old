using SlackApi.Core.Data.Message.Rtm.Request;
using SlackApi.Core.Interface.Rtm;

namespace SlackApi.Rtm.Factory
{
    internal class RtmRequestFactory : IRtmRequestFactory
    {
        private int pingMessageId;

        public PingRequest CreatePingRequest() => new PingRequest(pingMessageId++);
    }
}
