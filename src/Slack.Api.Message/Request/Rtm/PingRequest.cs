using Newtonsoft.Json;

namespace Slack.Api.Message.Request.Rtm
{
    public class PingRequest
    {
        public PingRequest(int id)
        {
            Id = id;
            Type = "ping";
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public string ToJsonString() => JsonConvert.SerializeObject(this);
    }
}
