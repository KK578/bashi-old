using Newtonsoft.Json;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class MessageResponse : BaseMessageResponse
    {
        [JsonProperty("source_team")]
        public string SourceTeam { get; set; }
    }
}
