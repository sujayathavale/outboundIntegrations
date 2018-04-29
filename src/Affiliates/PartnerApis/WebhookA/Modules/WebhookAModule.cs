using AffiliatesApis.Common.Functions;
using AffiliatesApis.WebhookA.Functions;
using Autofac;


namespace AffiliatesApis.WebhookA.Modules
{
    public class WebhookAModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebhookAFunction>()
                .Named<IFunction>("WebhookA").InstancePerLifetimeScope();            
        }
    }
}