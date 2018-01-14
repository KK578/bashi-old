using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public class UserTypingResponse : BaseRtmResponse
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
