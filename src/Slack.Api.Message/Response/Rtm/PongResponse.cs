using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public class PongResponse : BaseRtmResponse
    {
        [JsonProperty("reply_to")]
        public int PongId { get; set; }
    }
}
