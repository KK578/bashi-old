using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm
{
    public static class SocketDecoder
    {
        public static WebSocketResponse Deserialize(string json)
        {
            var response = RtmResponseFactory.CreateResponse(json);

            return new WebSocketResponse(response.Type, response);
        } 
    }
}
