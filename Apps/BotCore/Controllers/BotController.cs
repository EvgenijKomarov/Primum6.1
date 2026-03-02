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
        public async Task<ActionResult<BotOutput>> ProcedeTextCommand([FromBody] BotInput input, CancellationToken token)
            => await iterator.ProcessTextMessage(input, token);

        [HttpPost("callbackquery-command")]
        public async Task<ActionResult<BotOutput>> ProcedeCallBackQuery([FromBody] BotInput input, CancellationToken token)
            => await iterator.ProcessCallBackQuery(input, token);
    }
}
