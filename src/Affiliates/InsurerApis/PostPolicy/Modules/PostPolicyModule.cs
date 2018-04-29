using Autofac;
using InsuranceApis.Common.Functions;
using InsuranceApis.PostPolicy.Functions;
using InsuranceApis.PostPolicy.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Runtime.Caching;


namespace InsuranceApis.PostPolicy.Modules
{
    public class PostPolicyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var config = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("func.settings.json")
                             .Build();

            var postPolicySettings = new PostPolicySettings();
            config.GetSection("PostPolicySettings").Bind(postPolicySettings);

            builder.RegisterInstance(postPolicySettings).SingleInstance();

            builder.RegisterType<PostPolicyFunction>()
                .Named<IFunction>("PostPolicy").InstancePerLifetimeScope();

            builder.RegisterInstance(new HttpClient())
                .As<HttpClient>().SingleInstance();

            builder.RegisterInstance(MemoryCache.Default)
                .As<MemoryCache>().SingleInstance();            
        }
    }
}