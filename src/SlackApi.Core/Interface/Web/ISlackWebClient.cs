using System.Threading.Tasks;
using SlackApi.Core.Data.Message.Web.Response.Rtm;

namespace SlackApi.Core.Interface.Web
{
    public interface ISlackWebClient
    {
        Task<RtmConnectResponse> RtmConnectAsync(string token);
    }
}
