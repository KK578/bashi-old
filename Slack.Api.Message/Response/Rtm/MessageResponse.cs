using Newtonsoft.Json;

namespace Slack.Api.Message.Response.Rtm
{
    public class MessageResponse : BaseMessageResponse
    {
        [JsonProperty("source_team")]
        public string SourceTeam { get; set; }
    }
}
