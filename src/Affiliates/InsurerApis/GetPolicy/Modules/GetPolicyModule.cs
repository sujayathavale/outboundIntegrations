using Autofac;
using InsurerApis.Common.Functions;
using InsurerApis.GetPolicy.Functions;
using System.Net.Http;

namespace InsurerApis.GetPolicy.Modules
{
    /// <summary>
    /// Adds components and dependencies for GetPolicy function
    /// </summary>
    public class GetPolicyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetPolicyFunction>()
                .Named<IFunction>("GetPolicy").InstancePerLifetimeScope();
          
            builder.RegisterInstance(new HttpClient())
                .As<HttpClient>().SingleInstance();            
        }
    }
}