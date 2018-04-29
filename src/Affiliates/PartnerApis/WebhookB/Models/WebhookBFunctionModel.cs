using Microsoft.Azure.WebJobs.Extensions.EventGrid;

namespace AffiliatesApis.WebhookB.Models
{
    public class WebhookBFunctionModel
    {
        public EventGridEvent EventPayload { get; set; }
    }
}
