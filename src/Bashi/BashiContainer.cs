﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Autofac;
using Bashi.Config;
using Bashi.Core.Interface.Config;
using SlackApi.Core.Factory.Message;
using SlackApi.Rtm;
using SlackApi.Web;
using Bashi.Slack;
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
            builder.RegisterType<SlackRtmEventPublisher>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackRtmClient>().SingleInstance().AsImplementedInterfaces();

            // SlackApi.Web
            builder.RegisterType<SlackWebClient>().SingleInstance().AsImplementedInterfaces();

            // Bashi.Slack
            builder.RegisterType<SlackConnectionManager>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackEventLogger>().SingleInstance().AsImplementedInterfaces();

            // Bashi.Config
            var configFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bashi.config");
            builder.Register(c => new BashiConfigFile(configFilepath)).SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<EnvConfigParser>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<BashiConfigManager>().SingleInstance().AsImplementedInterfaces();
            builder.Register(c => c.Resolve<IBashiConfigManager>().SlackConfigGroup).SingleInstance().AsImplementedInterfaces();

            // Bashi
            builder.RegisterType<BashiApp>().SingleInstance().AsSelf();

            return builder.Build();
        }
    }
}
