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
    public class PublisherController(IPublisher publisher, SignServiceClient client) : DefaultController
    {
        [HttpPost("push-event")]
        public async Task<IActionResult> PushEvent([FromBody] IPushable inputEvent, CancellationToken cancellationToken)
        {
            if(inputEvent is IChatBotNotification chatBotNotification)
            {
                foreach (var adressant in chatBotNotification.ToChatBotNotifications())//для каждого адресата 
                {
                    foreach (var sign in await client.GetSignsAsync(adressant.Key))//для каждой подписи
                    {
                        await publisher.Publish(new ChatBotNotification
                        {
                            ChatSign = sign,
                            Text = adressant.Value
                        }, cancellationToken);
                    }
                }
            }
            else if(inputEvent is IMailNotification)//todo
            {

            }
            return Ok();
        }
    }
}
