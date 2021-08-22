using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UpStreamApi.Controllers
{
    public class CircuitBreakerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CircuitBreakerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> Status(int requestNumber)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44343/CircuitBreaker/Status");
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                
            }
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {

            }
            return Ok();
        }
    }
}
