using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FlakyApiBenchmark
{
    class Program
    {
        static readonly SemaphoreSlim Semaphore = new(100, 100);

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var requestsBag = new ConcurrentBag<CircuitBreakerFlakyApiRequest>();
            for (int requestId = 0; requestId < 500; requestId++)
            {
                requestsBag.Add(new CircuitBreakerFlakyApiRequest(requestId));
            }

            var tasks = new Task[requestsBag.Count];
            foreach (var newRequest in requestsBag)
            {
                await Semaphore.WaitAsync();
                tasks[newRequest.RequestId] = Task.Run(async () =>
                {
                    try
                    {
                        await MakeRequest(newRequest);
                    }
                    finally
                    {
                        Semaphore.Release();
                    }
                });
            }
            Task.WaitAll(tasks);
            Console.WriteLine(requestsBag.Count(x => x.ResponseType=="Close"));
            Console.WriteLine(requestsBag.Count(x => x.ResponseType== "HalfOpen"));
            Console.WriteLine(requestsBag.Count(x => x.ResponseType== "Open"));
        }

        private static async Task MakeRequest(CircuitBreakerFlakyApiRequest request)
        {
            var client = new HttpClient();
            request.RequestStartedAt = DateTime.UtcNow.Ticks;
            var response = await client.GetAsync("https://localhost:44343/Weather?city=tehran");
            if (response.IsSuccessStatusCode)
            {
                request.ResponseType = "Close";
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    request.ResponseType = "HalfOpen";
                else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                    request.ResponseType = "Open";
            }
            request.RequestFinishedAt = DateTime.UtcNow.Ticks;
        }
    }

    class CircuitBreakerFlakyApiRequest
    {
        public CircuitBreakerFlakyApiRequest(int requestId)
        {
            RequestId = requestId;
        }

        public int RequestId { get; }
        public long RequestStartedAt { get; set; }
        public long RequestFinishedAt { get; set; }
        public string ResponseType { get; set; }
    }
}
