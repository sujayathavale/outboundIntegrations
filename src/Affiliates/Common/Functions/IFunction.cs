using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Functions
{
    public interface IFunction
    {
        Task<TOutput> InvokeAsync<TPayload, TOutput>(TPayload payload);
    }
}
