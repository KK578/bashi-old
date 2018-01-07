using System;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi
{
    internal class BashiApp
    {
        public static void Main(string[] args)
        {
            var token = args[0];
            Test(token);
            
            Console.WriteLine("Back in Main.");
            Console.ReadLine();
        }

        private static async void Test(string token)
        {
            var webClient = new SlackWebClient();
            var connectResponse = await webClient.RtmConnectAsync(token);

            TestWebSocket(connectResponse.WebSocketUrl);
        }

        private static async void TestWebSocket(string url)
        {
            var rtmClient = new SlackRtmClient();
            await rtmClient.ConnectAsync(url);

            Console.WriteLine("Connected.");
        }
    }
}