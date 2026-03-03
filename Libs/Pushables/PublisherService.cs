using Pushables.Abstractions;
using Pushables.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables
{
    public class PublisherService(string publisherUrl, HttpClient httpClient)
    {
        private PublisherClient client = new PublisherClient(publisherUrl, httpClient);
        public async Task Push(IPushable message)
        {
            if (message is IChatBotNotification chatNotification)
            {
                await client.PushChatNotificationAsync(chatNotification.ToChatBotNotifications().ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value));
            }
            if (message is IMailNotification mailNotification) 
            { 
                await client.PushMailNotificationAsync(mailNotification.MailTitle, mailNotification.ToMailNotifications().ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value));
            }
        }
    }
}

