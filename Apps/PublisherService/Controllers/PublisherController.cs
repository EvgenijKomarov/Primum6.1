using Microsoft.AspNetCore.Mvc;
using Publisher.Services;
using Pushables;
using Pushables.Entities;
using Pushables.Events;
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
    public class PublisherController(IPublisher publisher) : DefaultController
    {
        [HttpPost("push-mail-notification")]
        public async Task<IActionResult> PushMailNotification([FromBody] IEnumerable<MailNotification> notifications, CancellationToken cancellationToken)
        {
            foreach (var notification in notifications)
            {
                await publisher.Publish(notification, cancellationToken);
            }
            return Ok();
        }

        [HttpPost("push-chatbot-notification")]
        public async Task<IActionResult> PushChatBotNotification([FromBody] IEnumerable<ChatBotNotification> notifications, CancellationToken cancellationToken)
        {
            foreach (var notification in notifications)
            {
                await publisher.Publish(notification, cancellationToken);
            }
            return Ok();
        }

        [HttpPost("push-user-verified-email-event")]
        public async Task<IActionResult> PushUserVerifiedEmailEvent([FromBody] UserVerifiedEmailEvent inputEvent, CancellationToken cancellationToken)
        {
            await publisher.Publish(inputEvent, cancellationToken);
            return Ok();
        }

        [HttpPost("push-user-verified-chat-event")]
        public async Task<IActionResult> PushUserVerifiedChatEvent([FromBody] UserVerifiedChatEvent inputEvent, CancellationToken cancellationToken)
        {
            await publisher.Publish(inputEvent, cancellationToken);
            return Ok();
        }
    }
}
