using BotCore.Entities;
using BotCore.Entities.Inputs;
using BotCore.Entities.Outputs;
using BotCore.Nodes;
using BotCore.Services.Iterators;
using Engine;
using Microsoft.AspNetCore.Mvc;

namespace BotCore.Controllers
{
    [ApiController]
    [Route("bot-core")]
    public class BotController(BotIterator iterator) : DefaultController
    {
        [HttpPost("text-command")]
        public async Task<ActionResult<OutputMessage>> ProcedeTextCommand([FromQuery] int? userId, [FromBody] string message, CancellationToken token)
            => await iterator.ProcessTextMessage(userId, message, token);

        [HttpPost("callbackquery-command")]
        public async Task<ActionResult<OutputMessage>> ProcedeCallBackQuery([FromQuery] int? userId, [FromBody] string message, CancellationToken token)
            => await iterator.ProcessCallBackQuery(userId, message, token);
    }
}
