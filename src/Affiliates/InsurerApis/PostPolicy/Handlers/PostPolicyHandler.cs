using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InsuranceApis.Common.Functions;
using InsuranceApis.PostPolicy.Models;
using InsuranceApis.PostPolicy.Modules;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InsuranceApis.PostPolicy.Handlers
{
    public static class PostPolicyHandler
    {
        public static IFunctionActivator FunctionActivator = new AutofacFunctionActivator(new PostPolicyModule());

        [FunctionName("PostPolicyHandler")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "api/policy/{policyId:long}")]HttpRequestMessage req,
            ILogger log)
        {
            try
            {
                var result = await FunctionActivator.Activate<IFunction>("PostPolicy")
                   .InvokeAsync<PostPolicyFunctionModel, string>(null, log).ConfigureAwait(false);

                return req.CreateResponse(HttpStatusCode.OK, $"Policy State is { result }");
            }
            catch (Exception ex)
            {
                log?.LogError($"Exception in function GetPolicyHandler -> { ex.GetBaseException().Message }");
            }

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
