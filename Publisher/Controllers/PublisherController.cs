using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Publisher.Registry;
using Publisher.Services;
using Pushables;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace Publisher.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController(IPublisher publisher, EventTypeRegistry registry) : DefaultController
    {
        [HttpPost("push")]
        public async Task<IActionResult> PushEvent([FromBody] JsonElement body, CancellationToken cancellationToken)
        {
            if (!body.TryGetProperty("type", out var typeProp) ||
            !body.TryGetProperty("data", out var dataProp))
            {
                return BadRequest(new { error = "Expected JSON with 'type' and 'data' properties" });
            }

            var typeName = typeProp.GetString();
            if (string.IsNullOrEmpty(typeName))
            {
                return BadRequest(new { error = "Type name is required" });
            }

            var eventType = registry.GetTypeByName(typeName);
            if (eventType == null)
            {
                return BadRequest(new { error = $"Unknown event type: {typeName}" });
            }

            // Десериализуем data в конкретный тип
            var message = JsonSerializer.Deserialize(dataProp.GetRawText(), eventType) as IPushable;
            if (message == null)
            {
                return BadRequest(new { error = "Failed to deserialize message" });
            }

            await publisher.Publish(message, cancellationToken);

            return Accepted(new { status = "accepted", type = typeName });
        }
    }
}
