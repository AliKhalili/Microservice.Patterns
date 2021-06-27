using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlakyApi.Implementations
{
    ///<inheritdoc/>
    public class DefaultService : IService
    {
        private readonly ILogger<DefaultService> _logger;

        public DefaultService(ILogger<DefaultService> logger)
        {
            _logger = logger;
        }

        public Task<DefaultResponse> DoSomething()
        {
            var result = new DefaultResponse { SystemStatus = "Up", SystemTicks = DateTime.UtcNow.Ticks };
            _logger.LogInformation("{0} is called at {1}", nameof(DoSomething), result.SystemTicks);
            return Task.FromResult(result);
        }
    }
}