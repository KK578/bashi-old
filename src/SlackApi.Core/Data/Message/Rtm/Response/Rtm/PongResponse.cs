using Newtonsoft.Json;
using SlackApi.Core.Base.Rtm;

namespace SlackApi.Core.Data.Message.Rtm.Response.Rtm
{
    public class PongResponse : BaseRtmResponse
    {
        [JsonProperty("reply_to")]
        public int PongId { get; set; }
    }
}
