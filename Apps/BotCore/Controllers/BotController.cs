using BotCore.Entities;
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
        public async Task<ActionResult<BotOutput>> ProcedeTextCommand([FromQuery] int? userId, [FromBody] BotInput input, CancellationToken token)
            => await iterator.ProcessTextMessage(userId, input, token);

        [HttpPost("callbackquery-command")]
        public async Task<ActionResult<BotOutput>> ProcedeCallBackQuery([FromQuery] int? userId, [FromBody] BotInput input, CancellationToken token)
            => await iterator.ProcessCallBackQuery(userId, input, token);
    }
}
