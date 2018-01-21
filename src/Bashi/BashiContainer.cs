using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using SlackApi.Rtm;
using SlackApi.Web;
using Bashi.Core.Connection;
using Bashi.Slack;
using Bashi.Slack.Connection;
using SlackApi.Rtm.Events;
using SlackApi.Rtm.Factory;

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
            builder.RegisterType<SlackRtmEventPublisher>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsImplementedInterfaces();

            // SlackApi.Web
            builder.RegisterType<SlackWebClient>().SingleInstance().AsImplementedInterfaces();

            // Bashi.Slack
            builder.RegisterType<SlackConnectionManager>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackEventLogger>().SingleInstance().AsImplementedInterfaces();

            // Bashi.Core
            builder.RegisterType<ConnectionParamsFactory>().SingleInstance().AsImplementedInterfaces();

            // Bashi
            builder.RegisterType<BashiApp>().SingleInstance().AsSelf();

            return builder.Build();
        }

        public BashiContainer RegisterEnvironment(string token)
        {
            var slackConnectionParams = new SlackConnectionParams(token);
            builder.RegisterInstance(slackConnectionParams).SingleInstance().AsImplementedInterfaces();

            return this;
        }
    }
}
