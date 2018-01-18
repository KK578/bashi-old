using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public abstract class BaseRtmResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
