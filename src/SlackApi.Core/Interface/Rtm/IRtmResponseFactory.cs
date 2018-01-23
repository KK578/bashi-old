using SlackApi.Core.Base.Rtm;
using SlackApi.Core.Data.Message.Rtm.Response.Rtm;

namespace SlackApi.Core.Interface.Rtm
{
    public interface IRtmResponseFactory
    {
        BaseRtmResponse CreateResponse(string json);
    }
}
