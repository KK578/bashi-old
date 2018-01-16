using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm
{
    public interface IRtmResponseFactory
    {
        BaseRtmResponse CreateResponse(string json);
    }
}
