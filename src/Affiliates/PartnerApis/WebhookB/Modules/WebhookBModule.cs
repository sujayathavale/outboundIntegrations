using AffiliatesApis.Common.Functions;
using AffiliatesApis.WebhookB.Functions;
using Autofac;


namespace AffiliatesApis.WebhookB.Modules
{
    public class WebhookBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebhookBFunction>()
                .Named<IFunction>("WebhookB").InstancePerLifetimeScope();
        }
    }
}