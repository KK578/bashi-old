using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class UserMessageResponse : BaseMessageResponse
    {
        [JsonProperty("user")]
        public string UserId { get; set; }
    }
}
