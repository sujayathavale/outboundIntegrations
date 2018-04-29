using Autofac;
using InsuranceApis.Common.Functions;
using InsuranceApis.GetPolicy.Functions;
using System.Net.Http;

namespace InsuranceApis.GetPolicy.Modules
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