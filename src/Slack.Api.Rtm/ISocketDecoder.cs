using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm
{
    public interface ISocketDecoder
    {
        WebSocketResponse Deserialize(string json);
    }
}
