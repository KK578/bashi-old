using Autofac;
using Bashi.Config;
using Bashi.Log;
using Bashi.Slack;
using SlackApi.Rtm;
using SlackApi.Web;

namespace Bashi
{
    public static class BashiContainer
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new BashiConfigModule());
            builder.RegisterModule(new BashiLogModule());
            builder.RegisterModule(new BashiSlackModule());
            builder.RegisterModule(new SlackApiRtmModule());
            builder.RegisterModule(new SlackApiWebModule());

            builder.RegisterType<BashiApp>().SingleInstance().AsSelf();

            return builder.Build();
        }
    }
}
