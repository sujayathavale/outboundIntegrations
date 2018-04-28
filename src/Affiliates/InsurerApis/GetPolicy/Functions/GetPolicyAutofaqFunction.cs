using Common.Functions;
using InsurerApis.GetPolicy.Models;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InsurerApis.GetPolicy.Functions
{
    public class GetPolicyAutofaqFunction : IFunction
    {
        public GetPolicyAutofaqFunction()
        {            
        }

        public async Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload)
        {
            try
            {
                //_log?.Info("GetPolicyAutofaqFunction invoked.");

                var model = payload as Models.GetPolicyFunctionModel;

               // some static representation for the policy
               object result = await Task.FromResult(new[]
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

                return (TOutput)result;
            }
            catch (Exception ex)
            {
                // log error message
                //_log?.Error($"Exception in function GetPolicyAutofaqFunction -> { ex.GetBaseException().Message }");
                
                // bubble up exception, so that function handler can perform common error handling
                throw;
            }            
        }
    }
}
