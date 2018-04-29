using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffiliatesApis.WebhookA.Models
{
    public class WebhookAFunctionModel
    {
        public EventGridEvent EventPayload { get; set; }
    }
}
