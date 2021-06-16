using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlakyApi.Utils.Strategy
{
    public class NoOperationFlakyStrategy : IFlakyStrategy
    {
        public Task<TResult> Execute<TResult>(Func<IDictionary<string, object>, Task<TResult>> func, IDictionary<string, object> parameters)
        {
            return func(parameters);
        }
    }
}