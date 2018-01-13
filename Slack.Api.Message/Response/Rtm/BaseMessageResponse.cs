using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public abstract class BaseMessageResponse : BaseRtmResponse
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public double Timestamp { get; set; }
    }
}