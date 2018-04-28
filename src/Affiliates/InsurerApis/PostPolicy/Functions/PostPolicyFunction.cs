using InsurerApis.Common.Functions;
using InsurerApis.PostPolicy.Models;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace InsurerApis.PostPolicy.Functions
{
    public class PostPolicyFunction : IFunction
    {
        private readonly HttpClient _httpClient;
        private readonly MemoryCache _memoryCache;
        private readonly PostPolicySettings _postPolicySettings;

        public PostPolicyFunction(HttpClient httpclient, MemoryCache memoryCache, PostPolicySettings postPolicySettings)
        {
            _httpClient = httpclient;
            _memoryCache = memoryCache;
            _postPolicySettings = postPolicySettings;
        }

        public async Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, TraceWriter log)
        {
            object result = null;

            try
            {
                log?.Info("PostPolicyFunction invoked.");

                var nextPolicyState = "Policy.Created";

                // read policy state from cache
                var cachedValue = _memoryCache?["policyState"];

                // if previous cached state present, transition to next state
                if (cachedValue != null)
                {
                    var currentPolicyState = (string)cachedValue;
                    log.Info($"currentPolicyState = {currentPolicyState}");                    
                    nextPolicyState = TransitionPolicyState(currentPolicyState);
                }

                log.Info($"nextPolicyState = {nextPolicyState}");

                if (nextPolicyState == "Policy.Created")
                {
                    await this.TriggerPolicyCreatedEventGridEvent();
                }

                if (nextPolicyState == "Policy.Refunded")
                {
                    await this.TriggerPolicyRefundedEventGridEvent();
                }

                // add updated policy state to cache
                _memoryCache?.Set("policyState", nextPolicyState, DateTimeOffset.Now.AddMinutes(30));

                result = nextPolicyState;
            }
            catch (Exception ex)
            {
                // log error message
                log?.Error($"Exception in function PostPolicyFunction -> { ex.GetBaseException().Message }");

                // bubble up exception, so that function handler can perform common error handling
                throw;
            }

            return (TOutput)result;
        }

        private string TransitionPolicyState(string currentPolicyState)
        {
            var nextPolicyState = "Policy.Created";

            switch (currentPolicyState)
            {
                case "Policy.Created":
                    nextPolicyState = "Policy.Fulfilled";
                    break;
                case "Policy.Fulfilled":
                    nextPolicyState = "Policy.PartialRefund";
                    break;
                case "Policy.PartialRefund":
                    nextPolicyState = "Policy.Refunded";
                    break;
                case "Policy.Refunded":
                    nextPolicyState = "Policy.Created";
                    break;
                default:
                    nextPolicyState = "Policy.Created";
                    break;
            }

            return nextPolicyState;
        }

        private async Task TriggerPolicyCreatedEventGridEvent()
        {
            var body = new[]
            {
                new {
                        id = "d67b5055-c25e-4755-916e-e8692000c9ed",
                        eventType = "Policy.Created",
                        subject = "urn:partnerId:Partner-A", // Partner-A is the subject
                        eventTime = "2018-03-30T03:00:00Z",
                        // example of a fat-ping, contains data fields in addition to self link 
                        data = new {
                            OrderId = "ORD12313",
                            Status = "Sold",
                            ClickId = "CID1234",
                            SaleDate = "2018-03-30T03:00:00Z",
                            Currency = "AUD",
                            SaleValue = "1200",
                            CommissionValue = "120",
                            _links = new []
                                {
                                    new
                                        {
                                            rel = "self",
                                            href = "https://insurerapis.azurewebsites.net/api/GetPolicyOperation?code=7bhf8yzcmaVRUBTPN0UUKfgVYKeXXyj8FmfGQlLHMIsusd0TPu7IAg=="
                                        }
                                }
                        },
                        dataVersion = "1.0"
                    }
            };

            var requestUrl = _postPolicySettings.PolicyCreated.Uri;

            _httpClient?.DefaultRequestHeaders.Accept.Clear();
            _httpClient?.DefaultRequestHeaders.Add("aeg-sas-key", _postPolicySettings.PolicyCreated.SaSKey);

            using (var message = await this._httpClient?.PostAsJsonAsync(requestUrl, body))
            {
                var postResult = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                // TODO - error handling - check for non-successful posts to event grid topic/event
            }            
        }

        private async Task TriggerPolicyRefundedEventGridEvent()
        {
            var body = new[]
            {
                new {
                        id = "d67b5055-c25e-4755-916e-e8692000c9ed",
                        eventType = "Policy.Refunded",
                        subject = "urn:partnerId:Partner-B", // Partner-B is the subject
                        eventTime = "2018-03-30T03:00:00Z",
                        // example of a light-ping, only contains self link that needs to be called to get details about the event
                        data = new {
                           _links = new []
                                {
                                    new
                                        {
                                            rel = "self",
                                            href = "https://insurerapis.azurewebsites.net/api/GetPolicyOperation?code=7bhf8yzcmaVRUBTPN0UUKfgVYKeXXyj8FmfGQlLHMIsusd0TPu7IAg=="
                                        }
                                }
                        },
                        dataVersion = "1.0"
                    }
            };

            var requestUrl = _postPolicySettings.PolicyRefunded.Uri;
            _httpClient?.DefaultRequestHeaders.Accept.Clear();
            _httpClient?.DefaultRequestHeaders.Add("aeg-sas-key", _postPolicySettings.PolicyRefunded.SaSKey);

            using (var message = await this._httpClient?.PostAsJsonAsync(requestUrl, body))
            {
                var postResult = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                // TODO - error handling - check for non-successful posts to event grid topic/event
            }           
        }        
    }    
}
