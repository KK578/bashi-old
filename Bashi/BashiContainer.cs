using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using Slack.Api.Rtm;
using Slack.Api.Web;

namespace Bashi
{
    public static class BashiContainer
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HttpClient>().SingleInstance().AsSelf();
            builder.RegisterType<ClientWebSocket>().AsSelf();

            builder.RegisterType<SocketDecoder>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<RtmResponseFactory>().SingleInstance().AsImplementedInterfaces();
            
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsSelf();
            builder.RegisterType<SlackWebClient>().SingleInstance().AsSelf();
            
            return builder.Build();
        }
    }
}
