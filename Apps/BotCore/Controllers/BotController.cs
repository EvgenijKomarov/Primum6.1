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
        /// <summary>
        /// Обработка текстовых сообщений
        /// </summary>
        /// <param name="input">Сущность, где ChatSign - подпись реализации (realizationTag - константа реализации, chatId - id чата. откуда пришло сообщение, username - юзернейм пользователя) и data - текст сообщения</param>
        /// <param name="token"></param>
        /// <returns>Сообщение, где Message - текст сообщения, а Buttons - словарь кнопок (ключ - текст кнопки, значение - команда CallBackQuery). Просто посылается в ответ</returns>
        [HttpPost("text-command")]
        public async Task<ActionResult<BotOutput>> ProcedeTextCommand([FromBody] BotInput input, CancellationToken token)
            => await iterator.ProcessTextMessage(input, token);

        /// <summary>
        /// Обработка нажатий кнопок
        /// </summary>
        /// <param name="input">Сущность, где ChatSign - подпись реализации (realizationTag - константа реализации, chatId - id чата. откуда пришло сообщение, username - юзернейм пользователя) и data - команда нажатия кнопки (CallbackQuery)</param>
        /// <param name="token"></param>
        /// <returns>Сообщение, где Message - текст сообщения, а Buttons - словарь кнопок (ключ - текст кнопки, значение - команда CallBackQuery). Заменяет сообщение, в котором была нажата соответствующая кнопка</returns>
        [HttpPost("callbackquery-command")]
        public async Task<ActionResult<BotOutput>> ProcedeCallBackQuery([FromBody] BotInput input, CancellationToken token)
            => await iterator.ProcessCallBackQuery(input, token);
    }
}
