using System;
using System.IO;
using Autofac;
using Bashi.Config.File;
using Bashi.Config.Parser;
using Bashi.Core.Interface.Config;

namespace Bashi.Config
{
    public class BashiConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var configFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bashi.config");
            builder.Register(c => new BashiConfigFile(configFilepath)).SingleInstance().AsImplementedInterfaces();

            builder.RegisterType<EnvConfigParser>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<BashiConfigManager>().SingleInstance().AsImplementedInterfaces();
            builder.Register(c => c.Resolve<IBashiConfigManager>().SlackConfigGroup).SingleInstance().AsImplementedInterfaces();
        }
    }
}
