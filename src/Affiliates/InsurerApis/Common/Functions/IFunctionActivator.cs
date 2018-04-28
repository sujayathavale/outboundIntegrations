using Microsoft.Azure.WebJobs.Host;

namespace InsurerApis.Common.Functions
{
    public interface IFunctionActivator
    {
        TFunction Activate<TFunction>(string name = null)
            where TFunction : IFunction;
    }
}
