using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlakyApi.Implementations
{
    public interface IFlakyStrategy
    {
        Task<TResult> Execute<TResult>(Func<IDictionary<string, object>, Task<TResult>> func, IDictionary<string, object> parameters);
    }
}