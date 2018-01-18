using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class BotMessageResponse : MessageResponse
    {
        [JsonProperty("bot_id")]
        public string BotId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
