using SlackApi.Core.Base.Rtm;

namespace SlackApi.Core.Interface.Rtm
{
    public interface IRtmResponseFactory
    {
        BaseRtmResponse CreateResponse(string json);
    }
}
