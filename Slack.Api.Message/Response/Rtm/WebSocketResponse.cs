namespace Slack.Api.Message.Response.Rtm
{
    public class WebSocketResponse
    {
        public WebSocketResponse(string type, BaseRtmResponse response)
        {
            Type = type;
            Response = response;
        }

        public string Type { get; }
        public BaseRtmResponse Response { get; }

        public override string ToString() => $"[{Type}] {Response}";
    }
}