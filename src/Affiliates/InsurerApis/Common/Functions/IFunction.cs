using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace InsurerApis.Common.Functions
{
    public interface IFunction
    {
        Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload, TraceWriter log);
    }
}
