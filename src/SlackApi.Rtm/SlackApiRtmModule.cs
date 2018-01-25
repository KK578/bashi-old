using System.Net.WebSockets;
using Autofac;
using SlackApi.Rtm.Client;
using SlackApi.Rtm.Events;
using SlackApi.Rtm.Factory;

namespace SlackApi.Rtm
{
    public class SlackApiRtmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ClientWebSocket>().AsSelf();

            builder.RegisterType<RtmRequestFactory>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<RtmResponseFactory>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackConnectionEventPublisher>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackRtmEventPublisher>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<WebSocketManager>().SingleInstance().AsImplementedInterfaces();
        }
    }
}
