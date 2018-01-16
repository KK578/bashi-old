using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Web.Rtm
{
    public class RtmConnectResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("url")]
        public string WebSocketUrl { get; set; }
    }
}
