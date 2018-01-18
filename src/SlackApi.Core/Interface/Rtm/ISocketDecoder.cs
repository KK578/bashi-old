using SlackApi.Core.Data.Message.Rtm.Response.Rtm;

namespace SlackApi.Core.Interface.Rtm
{
    public interface ISocketDecoder
    {
        WebSocketResponse Deserialize(string json);
    }
}
