using Newtonsoft.Json;

namespace SlackApi.Core.Base.Rtm
{
    public abstract class BaseRtmResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
