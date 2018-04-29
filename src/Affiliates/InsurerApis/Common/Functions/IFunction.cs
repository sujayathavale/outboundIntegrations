using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace InsuranceApis.Common.Functions
{
    public interface IFunction
    {
        Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, ILogger log);
    }
}
