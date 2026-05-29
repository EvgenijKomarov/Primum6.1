using CoreConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Authorize]
    [Route("user/chat-signs")]
    public class UserChatSignsController(UserClient client) : DefaultController
    {
        /// <summary>
        /// Все пользовательские чат-подписи. По ним можно узнать в каких чат-ботах пользователь подтвердил аутентификацию
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ChatSignPageResult>> GetChatSigns([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await client.ChatSignsAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Добавляет чат-подпись для пользователя
        /// </summary>
        /// <param name="token">Токен из чат-бота</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ChatSign>> ConfirmChatSign([FromBody] string token = null!)
            => Ok(await client.ConfirmChatAsync(User.GetUserId(), token));
    }
}
