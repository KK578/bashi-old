using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slack.Api.Message.Response.Web.Rtm;

namespace Slack.Api.Web
{
    public class SlackWebClient
    {
        private readonly HttpClient httpClient;

        public SlackWebClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RtmConnectResponse> RtmConnectAsync(string token)
        {
            const string url = "https://slack.com/api/rtm.connect";

            var values = new Dictionary<string, string>
            {
                {"token", token}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var connectResponse = JsonConvert.DeserializeObject<RtmConnectResponse>(responseContent);

            return connectResponse;
        }
    }
}
