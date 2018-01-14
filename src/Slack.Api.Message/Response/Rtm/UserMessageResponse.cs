using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public class UserMessageResponse : BaseMessageResponse
    {
        [JsonProperty("user")]
        public string UserId{ get; set; }
    }
}
