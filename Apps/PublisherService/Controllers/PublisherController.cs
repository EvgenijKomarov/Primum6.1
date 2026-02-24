using Microsoft.AspNetCore.Mvc;
using Publisher.Services;
using Pushables;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
namespace Publisher.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController(IPublisher publisher) : DefaultController
    {
        [HttpPost("push")]
        public async Task<IActionResult> PushEvent([FromQuery] string queue, [FromBody] string jsonBody, CancellationToken cancellationToken)
        {
            await publisher.Publish(queue, jsonBody, cancellationToken);
            return Ok();
        }
    }
}
