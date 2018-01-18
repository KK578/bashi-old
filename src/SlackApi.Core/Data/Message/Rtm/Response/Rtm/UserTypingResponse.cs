using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class UserTypingResponse : BaseRtmResponse
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
}
