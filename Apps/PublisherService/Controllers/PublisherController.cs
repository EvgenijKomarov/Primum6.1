using CoreConnection;
using Microsoft.AspNetCore.Mvc;
using Publisher.Services;
using PublisherService.Entities;
using Pushables;
using Pushables.Abstractions;
using Pushables.Events;
using SignServiceConnection;
using System;
using System.Collections;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
namespace Publisher.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController(IPublisher publisher, SignServiceClient signClient, UserClient userClient) : DefaultController
    {
        /// <summary>
        /// Запушить уведомление в чат боты
        /// </summary>
        /// <param name="inputKeyValue">Словарь, где ключ - id пользователя, а значение - текст сообщения</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("push-chat-notification")]
        public async Task<IActionResult> PushChatNotification([FromBody] Dictionary<int, string> inputKeyValue, CancellationToken cancellationToken)
        {
            foreach (var adressant in inputKeyValue)//для каждого адресата 
            {
                foreach (var sign in await signClient.GetSignsAsync(adressant.Key))//для каждой подписи
                {
                    await publisher.Publish(new ChatBotNotification
                    {
                        ChatSign = sign,
                        Text = adressant.Value
                    }, cancellationToken);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Запушить письмо на почту
        /// </summary>
        /// <param name="title">Тема письма</param>
        /// <param name="inputKeyValue">Словарь, где ключ - id пользователя, а значение - текст письма</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("push-mail-notification")]
        public async Task<IActionResult> PushMailNotification([FromQuery] string title, [FromBody] Dictionary<int, string> inputKeyValue, CancellationToken cancellationToken)
        {
            foreach (var adressant in inputKeyValue)//для каждого адресата 
            {
                await publisher.Publish(new EmailNotification
                {
                    MailAdress = await userClient.GetMailAsync(adressant.Key),
                    Text = adressant.Value,
                    Title = title
                }, cancellationToken);
            }
            return Ok();
        }
    }
}
