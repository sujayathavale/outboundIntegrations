using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InsuranceApis.Common.Functions;
using InsuranceApis.GetPolicy.Models;
using InsuranceApis.GetPolicy.Modules;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InsuranceApis.GetPolicy.Handlers
{
    public static class GetPolicyHandler
    {
        public static IFunctionActivator FunctionActivator = new AutofacFunctionActivator(new GetPolicyModule());

        [FunctionName("GetPolicyHandler")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/policy/{policyId:long}")]HttpRequestMessage req,
            ILogger log,
            long policyId)
        {
            try
            {
                var result = await FunctionActivator.Activate<IFunction>("GetPolicy")
                   .InvokeAsync<GetPolicyFunctionModel, object>(

                       // TODO - this could be moved out to a model factory/binder, when re-use becomes necessary
                       new GetPolicyFunctionModel()
                       {
                           PolicyId = policyId,
                           ResourceUri = req.RequestUri
                       }
                   , log).ConfigureAwait(false);

                return req.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                log?.LogError($"Exception in function GetPolicyHandler -> { ex.GetBaseException().Message }");                
            }          

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
