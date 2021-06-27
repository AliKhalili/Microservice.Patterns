using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlakyApi.Implementations
{
    public class DefaultFlakyService : IService
    {
        private readonly ILogger<DefaultFlakyService> _logger;
        private readonly IService _service;
        private readonly IFlakyStrategy _flakyStrategy;
        public DefaultFlakyService(ILogger<DefaultFlakyService> logger, DefaultService service, IFlakyStrategy flakyStrategy)
        {
            _logger = logger;
            _service = service;
            _flakyStrategy = flakyStrategy;
        }

        public async Task<DefaultResponse> DoSomething()
        {
            return await _flakyStrategy.Execute(async ctx => await _service.DoSomething(), null);
        }
    }
}