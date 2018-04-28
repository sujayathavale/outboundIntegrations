using Autofac;
using InsurerApis.Common.Functions;
using InsurerApis.PostPolicy.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace InsurerApis.PostPolicy.Modules
{
    public class PostPolicyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostPolicyFunction>()
                .Named<IFunction>("PostPolicy").InstancePerLifetimeScope();

            builder.RegisterInstance(new HttpClient())
                .As<HttpClient>().SingleInstance();

            builder.RegisterInstance(MemoryCache.Default)
                .As<MemoryCache>().SingleInstance();            
        }
    }
}