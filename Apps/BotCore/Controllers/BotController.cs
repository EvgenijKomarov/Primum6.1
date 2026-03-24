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
        {
    if (input.Data == "/start")
    {
        return new BotOutput
        {
            Message = "Добро пожаловать в школу программирования 🚀",
            Buttons = new Dictionary<string, string>()
            {
                {"Python курс","python"},
                {"C# курс","csharp"},
                {"Регистрация","register"}
            }
        };
    }

    if (input.Data == "python")
    {
        return new BotOutput
        {
            Message = "Курс Python 🐍\nВозраст: 10+\nДлительность: 3 месяца",
            Buttons = new Dictionary<string, string>
            {
                {"Записаться","register"}
            }
        };
    }

    if (input.Data == "csharp")
    {
        return new BotOutput
        {
            Message = "Курс C# 💻\nВозраст: 12+\nДлительность: 4 месяца",
            Buttons = new Dictionary<string, string>
            {
                {"Записаться","register"}
            }
        };
    }

    if (input.Data == "register")
    {
        return new BotOutput
        {
            Message = "Введите ваше имя:"
        };
    }

    return new BotOutput
    {
        Message = "Я не понял 😅 Напиши /start"
    };
}
            
        /// <summary>
        /// Обработка нажатий кнопок
        /// </summary>
        /// <param name="input">Сущность, где ChatSign - подпись реализации (realizationTag - константа реализации, chatId - id чата. откуда пришло сообщение, username - юзернейм пользователя) и data - команда нажатия кнопки (CallbackQuery)</param>
        /// <param name="token"></param>
        /// <returns>Сообщение, где Message - текст сообщения, а Buttons - словарь кнопок (ключ - текст кнопки, значение - команда CallBackQuery). Заменяет сообщение, в котором была нажата соответствующая кнопка</returns>
        [HttpPost("callbackquery-command")]
public async Task<ActionResult<BotOutput>> ProcedeCallBackQuery([FromBody] BotInput input, CancellationToken token)
{
    return await ProcedeTextCommand(input, token);
}
    }
}
