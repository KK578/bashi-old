using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public class BotMessageResponse : MessageResponse
    {
        [JsonProperty("bot_id")]
        public string BotId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
