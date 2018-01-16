using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using Bashi.Slack;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi
{
    public static class BashiContainer
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            // System.Net.Http
            builder.RegisterType<HttpClient>().SingleInstance().AsSelf();

            // System.Net.WebSockets
            builder.RegisterType<ClientWebSocket>().AsSelf();

            // Slack.Api.Rtm
            builder.RegisterType<SocketDecoder>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<RtmResponseFactory>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsSelf();

            // Slack.Api.Web
            builder.RegisterType<SlackWebClient>().SingleInstance().AsSelf();

            // Bashi.Slack
            builder.RegisterType<BashiSlackConnectionManager>().SingleInstance().AsImplementedInterfaces();

            // Bashi
            builder.RegisterType<BashiConnectionParamsFactory>().SingleInstance().AsSelf();
            builder.RegisterType<BashiApp>().AsSelf();

            RegisterEnvironment(builder);

            return builder.Build();
        }

        private static void RegisterEnvironment(ContainerBuilder builder)
        {
            var token = "To be read from Environment";
            var slackConnectionParams = new SlackConnectionParams(token);
            builder.RegisterInstance(slackConnectionParams).SingleInstance().AsSelf();
        }
    }
}
