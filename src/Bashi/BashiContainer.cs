using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using Bashi.Core.Connection;
using Bashi.Slack.Connection;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi
{
    public class BashiContainer
    {
        private readonly ContainerBuilder builder;

        public BashiContainer()
        {
            builder = new ContainerBuilder();
        }

        public IContainer Build()
        {
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
            builder.RegisterType<SlackConnectionManager>().SingleInstance().AsImplementedInterfaces();

            // Bashi
            builder.RegisterType<ConnectionParamsFactory>().SingleInstance().AsSelf();
            builder.RegisterType<BashiApp>().AsSelf();

            return builder.Build();
        }

        public BashiContainer RegisterEnvironment(string token)
        {
            var slackConnectionParams = new SlackConnectionParams(token);
            builder.RegisterInstance(slackConnectionParams).SingleInstance().AsSelf();

            return this;
        }
    }
}
