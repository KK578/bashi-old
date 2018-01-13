using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public class BaseRtmResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
