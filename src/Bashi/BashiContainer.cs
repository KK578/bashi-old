using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using Bashi.Config;
using Bashi.Core.Interface.Config;
using Bashi.Log;
using Bashi.Slack;
using Bashi.Slack.Connection;
using Bashi.Slack.Log;
using SlackApi.Rtm;
using SlackApi.Rtm.Client;
using SlackApi.Rtm.Events;
using SlackApi.Rtm.Factory;
using SlackApi.Web;

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

            // SlackApi.Rtm
            builder.RegisterModule(new SlackApiRtmModule());

            // SlackApi.Web
            builder.RegisterType<SlackWebClient>().SingleInstance().AsImplementedInterfaces();

            builder.RegisterModule(new BashiSlackModule());

            builder.RegisterModule(new BashiLogModule());

            builder.RegisterModule(new BashiConfigModule());

            // Bashi
            builder.RegisterType<BashiApp>().SingleInstance().AsSelf();

            return builder.Build();
        }
    }
}
