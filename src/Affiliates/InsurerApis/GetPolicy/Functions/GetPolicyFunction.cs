using InsuranceApis.Common.Functions;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace InsuranceApis.GetPolicy.Functions
{
    public class GetPolicyFunction : IFunction
    {
        public GetPolicyFunction()
        {            
        }

        public async Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, ILogger log)
        {
            object result = null;

            try
            {
                log?.LogInformation("GetPolicyFunction invoked.");

                var model = payload as Models.GetPolicyFunctionModel;

                if (model is null)
                    throw new ArgumentException("TPayload must be of type GetPolicyFunctionModel");

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
                log?.LogError($"Exception in function GetPolicyFunction -> { ex.GetBaseException().Message }");
                
                // bubble up exception, so that function handler can perform common error handling
                throw;
            }

            return (TOutput)result;
        }
    }
}
