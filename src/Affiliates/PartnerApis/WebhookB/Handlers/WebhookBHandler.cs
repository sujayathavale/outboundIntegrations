using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using AffiliatesApis.Common.Functions;
using System;
using AffiliatesApis.WebhookB.Modules;
using AffiliatesApis.WebhookB.Models;

namespace AffiliatesApis.WebhookB.Handlers
{
    public static class WebhookBHandler
    {
        public static IFunctionActivator FunctionActivator = new AutofacFunctionActivator(new WebhookBModule());

        [FunctionName("WebhookBHandler")]
        public static async void Run([EventGridTrigger]EventGridEvent eventGridEvent, TraceWriter log)
        {
            log?.Info($"Function WebhookBHandler invoked.");

            try
            {
                var result = await FunctionActivator.Activate<IFunction>("WebhookB")
                   .InvokeAsync<WebhookBFunctionModel, bool>(new WebhookBFunctionModel() { EventPayload = eventGridEvent }, log).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log?.Error($"Exception in function WebhookBHandler -> { ex.GetBaseException().Message }");
            }

            return;
        }
    }
}
