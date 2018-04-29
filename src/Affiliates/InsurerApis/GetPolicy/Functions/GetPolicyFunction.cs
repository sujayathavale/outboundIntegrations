using InsuranceApis.Common.Functions;
using InsuranceApis.GetPolicy.Models;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InsuranceApis.GetPolicy.Functions
{
    public class GetPolicyFunction : IFunction
    {
        public GetPolicyFunction()
        {            
        }

        public async Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, TraceWriter log)
        {
            object result = null;

            try
            {
                log?.Info("GetPolicyFunction invoked.");

                var model = payload as Models.GetPolicyFunctionModel;

               // some static representation for the policy
               result = await Task.FromResult(new[]
               {
                    new {
                        id = model.PolicyId,
                        status = "Policy.Refunded",
                        eventTime = DateTime.UtcNow,
                        data = new {
                            _links = new []
                                {
                                    new
                                        {
                                            rel = "self",
                                            href = model.ResourceUri
                                        }
                                }
                        },
                        dataVersion = "1.0"
                    }
               });                
            }
            catch (Exception ex)
            {
                // log error message
                log?.Error($"Exception in function GetPolicyFunction -> { ex.GetBaseException().Message }");
                
                // bubble up exception, so that function handler can perform common error handling
                throw;
            }

            return (TOutput)result;
        }
    }
}
