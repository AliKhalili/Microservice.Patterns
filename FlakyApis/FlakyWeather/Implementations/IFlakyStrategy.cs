using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlakyApi.Implementations
{
    /// <summary>
    /// Define an interface for changing the behavior of a function to flaky.
    /// <para> flaky here means an unreliable function that when it called there is a chance raise <see cref="ServiceCurrentlyUnavailableException"/> exception </para>
    /// </summary>
    public interface IFlakyStrategy
    {
        /// <summary>
        /// A wrapper for function which we want to turn its behavior for flaky.
        /// </summary>
        /// <typeparam name="TResult">the return type of given function</typeparam>
        /// <param name="func"></param>
        /// <param name="parameters">the input parameters of given function</param>
        /// <returns>the return type of given function</returns>
        /// <exception cref="ServiceCurrentlyUnavailableException"> Thrown when the flaky strategy want to change behavior to flaky</exception>
        Task<TResult> Execute<TResult>(Func<IDictionary<string, object>, Task<TResult>> func, IDictionary<string, object> parameters);
        
        /// <summary>
        /// After a while we can rest the flaky behavior to initial version
        /// </summary>
        /// <returns>Task</returns>
        Task Reset();
    }
}