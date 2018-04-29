using AffiliatesApis.Common.Functions;
using AffiliatesApis.WebhookA.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Threading.Tasks;

namespace AffiliatesApis.WebhookA.Functions
{
    public class WebhookAFunction : IFunction
    {
        public async Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, TraceWriter log)
        {
            object result = false;

            try
            {
                log?.Info("WebhookAFunction invoked.");

                var eventGridEvent = (payload as WebhookAFunctionModel)?.EventPayload;

                log?.Info($"Webhook A Fired" +
                 $"\n\tId:{eventGridEvent?.Id}" +
                 $"\n\tTopic:{eventGridEvent?.Topic}" +
                 $"\n\tSubject:{eventGridEvent?.Subject}" +
                 $"\n\tType:{eventGridEvent?.EventType}" +
                 $"\n\tData:{eventGridEvent?.Data}");


                result = true;
            }
            catch (Exception ex)
            {
                // log error message
                log?.Error($"Exception in function WebhookAFunction -> { ex.GetBaseException().Message }");

                // bubble up exception, so that function handler can perform common error handling
                throw;
            }

            return await Task.FromResult((TOutput)result);
        }
    }
}
