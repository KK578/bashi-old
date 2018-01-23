using Autofac;
using SlackApi.Web.Client;

namespace SlackApi.Web
{
    public class SlackApiWebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SlackWebClient>().SingleInstance().AsImplementedInterfaces();
        }
    }
}
