using Slack.Api.Message.Response.Rtm;

namespace Slack.Api.Rtm
{
    public class SocketDecoder : ISocketDecoder
    {
        private readonly IRtmResponseFactory factory;

        public SocketDecoder(IRtmResponseFactory factory)
        {
            this.factory = factory;
        }

        public WebSocketResponse Deserialize(string json)
        {
            var response = factory.CreateResponse(json);

            return new WebSocketResponse(response.Type, response);
        }
    }
}
