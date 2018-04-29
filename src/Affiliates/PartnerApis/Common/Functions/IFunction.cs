using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace AffiliatesApis.Common.Functions
{
    public interface IFunction
    {
        Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, TraceWriter log);
    }
}
