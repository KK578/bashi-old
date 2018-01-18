using SlackApi.Core.Data.Message.Rtm.Request;

namespace SlackApi.Core.Interface.Rtm
{
    public interface IRtmRequestFactory
    {
        PingRequest CreatePingRequest();
    }
}
