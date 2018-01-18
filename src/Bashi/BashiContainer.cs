using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using SlackApi.Core.Factory.Message;
using SlackApi.Rtm;
using SlackApi.Web;
using Bashi.Core.Connection;
using Bashi.Slack.Connection;

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

            // SlackApi.Core
            builder.RegisterType<RtmRequestFactory>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<RtmResponseFactory>().SingleInstance().AsImplementedInterfaces();

            // SlackApi.Rtm
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsImplementedInterfaces();

            // SlackApi.Web
            builder.RegisterType<SlackWebClient>().SingleInstance().AsImplementedInterfaces();

            // Bashi.Slack
            builder.RegisterType<SlackConnectionManager>().SingleInstance().AsImplementedInterfaces();

            // Bashi
            builder.RegisterType<ConnectionParamsFactory>().SingleInstance().AsSelf();
            builder.RegisterType<BashiApp>().SingleInstance().AsSelf();

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
