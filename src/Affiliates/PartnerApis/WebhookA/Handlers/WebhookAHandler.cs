using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using AffiliatesApis.Common.Functions;
using AffiliatesApis.WebhookA.Models;
using System;
using AffiliatesApis.WebhookA.Modules;

namespace AffiliatesApis.WebhookA.Handlers
{
    public static class WebhookAHandler
    {
        public static IFunctionActivator FunctionActivator = new AutofacFunctionActivator(new WebhookAModule());

        [FunctionName("WebhookAHandler")]
        public static async void Run([EventGridTrigger]EventGridEvent eventGridEvent, TraceWriter log)
        {
            log?.Info($"Function WebhookAHandler invoked.");

            try
            {            
                var result = await FunctionActivator.Activate<IFunction>("WebhookA")
                   .InvokeAsync<WebhookAFunctionModel, bool>(new WebhookAFunctionModel() { EventPayload = eventGridEvent } , log).ConfigureAwait(false);                
            }
            catch (Exception ex)
            {
                log?.Error($"Exception in function WebhookAHandler -> { ex.GetBaseException().Message }");
            }

            return;
        }
    }
}
