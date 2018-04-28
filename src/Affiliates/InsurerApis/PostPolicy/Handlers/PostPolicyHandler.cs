using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InsurerApis.Common.Functions;
using InsurerApis.GetPolicy.Models;
using InsurerApis.PostPolicy.Models;
using InsurerApis.PostPolicy.Modules;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace InsurerApis.PostPolicy.Handlers
{
    public static class PostPolicyHandler
    {
        public static IFunctionActivator FunctionActivator = new AutofacFunctionActivator(new PostPolicyModule());

        [FunctionName("PostPolicyHandler")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "api/policy/{policyId:long}")]HttpRequestMessage req,
            TraceWriter log)
        {
            try
            {
                var result = await FunctionActivator.Activate<IFunction>("PostPolicy")
                   .InvokeAsync<PostPolicyFunctionModel, string>(null, log).ConfigureAwait(false);

                return req.CreateResponse(HttpStatusCode.OK, $"Policy State is { result }");
            }
            catch (Exception ex)
            {
                log?.Error($"Exception in function GetPolicyHandler -> { ex.GetBaseException().Message }");
            }

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
