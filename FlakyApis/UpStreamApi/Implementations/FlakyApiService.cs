using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;

namespace UpStreamApi.Implementations
{
    public class FlakyApiService
    {
        
    }

    public class FlakyApiServicePolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(5));
        }
    }
}