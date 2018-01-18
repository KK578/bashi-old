using System.Threading.Tasks;

namespace SlackApi.Core.Interface.Rtm
{
    public interface ISlackRtmClient
    {
        Task ConnectAsync(string url);
    }
}
