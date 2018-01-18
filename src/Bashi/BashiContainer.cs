using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using SlackApi.Rtm;
using SlackApi.Web;

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

            // SlackApi.Rtm
            builder.RegisterType<RtmResponseFactory>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsImplementedInterfaces();

            // SlackApi.Web
            builder.RegisterType<SlackWebClient>().SingleInstance().AsImplementedInterfaces();

            // Bashi
            builder.RegisterType<BashiApp>().SingleInstance().AsSelf();

            return builder.Build();
        }
    }
}
