using Autofac;
using Bashi.Slack.Connection;
using Bashi.Slack.Log;

namespace Bashi.Slack
{
    public class BashiSlackModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SlackConnectionManager>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<SlackEventLogger>().SingleInstance().AsImplementedInterfaces();
        }
    }
}
