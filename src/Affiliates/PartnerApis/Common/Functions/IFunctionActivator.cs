using Microsoft.Azure.WebJobs.Host;

namespace AffiliatesApis.Common.Functions
{
    public interface IFunctionActivator
    {
        TFunction Activate<TFunction>(string name = null)
            where TFunction : IFunction;
    }
}
