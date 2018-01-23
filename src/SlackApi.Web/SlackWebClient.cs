using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bashi.Core.Interface.Log;
using Newtonsoft.Json;
using SlackApi.Core.Data.Message.Web.Response.Rtm;
using SlackApi.Core.Interface.Web;

namespace SlackApi.Web
{
    public class SlackWebClient : ISlackWebClient
    {
        private readonly HttpClient httpClient;
        private readonly IBashiLogger log;

        public SlackWebClient(HttpClient httpClient, IBashiLogger log)
        {
            this.httpClient = httpClient;
            this.log = log;
        }

        public async Task<RtmConnectResponse> RtmConnectAsync(string token)
        {
            const string url = "https://slack.com/api/rtm.connect";

            var values = new Dictionary<string, string> { { "token", token } };
            var content = new FormUrlEncodedContent(values);

            log.Info($"POST {url} - Sent");
            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            log.Info($"POST {url} - {responseContent}");

            var connectResponse = JsonConvert.DeserializeObject<RtmConnectResponse>(responseContent);

            return connectResponse;
        }
    }
}
