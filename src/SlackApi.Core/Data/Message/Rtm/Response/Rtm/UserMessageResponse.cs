using Newtonsoft.Json;
using SlackApi.Core.Base.Rtm;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class UserMessageResponse : BaseMessageResponse
    {
        [JsonProperty("user")]
        public string UserId { get; set; }
    }
}
