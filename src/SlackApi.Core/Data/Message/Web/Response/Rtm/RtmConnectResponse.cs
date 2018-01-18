using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Web.Response.Rtm
{
    public class RtmConnectResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("url")]
        public string WebSocketUrl { get; set; }
    }
}
