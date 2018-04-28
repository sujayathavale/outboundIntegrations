using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace InsurerApis
{
    public static class GetPolicyHandler
    {
        [FunctionName("GetPolicy")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/policy/{policyId:long}")]HttpRequestMessage req,
            TraceWriter log,
            long policyId)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                // some static representation for the policy
                var body = new[]
                {
                    new {
                        id = policyId,
                        status = "Policy.Refunded",
                        eventTime = DateTime.UtcNow,
                        data = new {
                            _links = new []
                                {
                                    new
                                        {
                                            rel = "self",
                                            href = req.RequestUri
                                        }
                                }
                        },
                        dataVersion = "1.0"
                    }
                };

                return req.CreateResponse(HttpStatusCode.OK, body);
            }
            catch (Exception ex)
            {
                log.Error("Exception");
                throw;
            }
        }
    }
}
