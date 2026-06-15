using Microsoft.Extensions.Logging;
using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Events;
using SolutionConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PublishServiceConnection
{
    public class PublisherService(ServiceRoutes routes, HttpClient httpClient, ILogger<PublisherService> logger)
    {
        public async Task Push(IPushable message)
        {
            if (message is IChatBotNotification chatNotification)
            {
                foreach(var notif in chatNotification.ToChatBotNotifications())
                {
                    await PushNotification(notif.Key, notif.Value, routes.ChatBotNotificationService.PublicUrl);
                }
            }
            if (message is IMailNotification mailNotification) 
            {
                foreach (var notif in mailNotification.ToMailNotifications())
                {
                    await PushNotification(notif.Key, notif.Value, routes.EmailNotificationService.PublicUrl);
                }
            }
            if (message is ICommonNotification commonNotification)
            {
                foreach (var notif in commonNotification.ToCommonNotifications())
                {
                    await PushNotification(notif.Key, notif.Value, routes.CommonNotificationService.PublicUrl);
                }
            }
        }

        private async Task PushNotification(int userId, string message, string route)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(route + $"/publish?userId={userId}&message={Uri.EscapeDataString(message)}", content: null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
            }
        }
    }
}

