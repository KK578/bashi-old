using Newtonsoft.Json;
using SlackApi.Core.Base.Rtm;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class BotMessageResponse : BaseMessageResponse
    {
        [JsonProperty("source_team")]
        public string SourceTeam { get; set; }

        [JsonProperty("bot_id")]
        public string BotId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
