using Pushables.Events;
using Pushables.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables
{
    public class PublisherService(string url, HttpClient httpClient)
    {
        private PublisherClient client = new PublisherClient(url, httpClient);
        public async Task Push<TPushable>(TPushable message) where TPushable : IPushable
        {
            if (message is IChatBotNotification chatBotNotification)
            {
                await client.PushChatbotNotificationAsync(chatBotNotification.GetChatBotNotifications());
            }
            if (message is IMailNotification mailNotification)
            { 
                await client.PushMailNotificationAsync(mailNotification.GetMailNotifications());
            }
            if (message is UserVerifiedEmailEvent userVerifiedEmailEvent)
            {
                await client.PushUserVerifiedEmailEventAsync(userVerifiedEmailEvent);
            }
        }
    }
}

