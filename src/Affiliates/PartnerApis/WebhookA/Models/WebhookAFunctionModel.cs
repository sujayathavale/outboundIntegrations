﻿using Microsoft.Azure.WebJobs.Extensions.EventGrid;

namespace AffiliatesApis.WebhookA.Models
{
    public class WebhookAFunctionModel
    {
        public EventGridEvent EventPayload { get; set; }
    }
}
