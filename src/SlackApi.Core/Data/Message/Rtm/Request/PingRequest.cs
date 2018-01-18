using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Rtm.Request
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
