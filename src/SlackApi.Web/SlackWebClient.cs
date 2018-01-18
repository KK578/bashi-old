﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SlackApi.Core.Data.Message.Web.Response.Rtm;
using SlackApi.Core.Interface.Web;

namespace SlackApi.Web
{
    public class SlackWebClient : ISlackWebClient
    {
        private readonly HttpClient httpClient;

        public SlackWebClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RtmConnectResponse> RtmConnectAsync(string token)
        {
            const string url = "https://slack.com/api/rtm.connect";

            var values = new Dictionary<string, string> { { "token", token } };

            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var connectResponse = JsonConvert.DeserializeObject<RtmConnectResponse>(responseContent);

            return connectResponse;
        }
    }
}
