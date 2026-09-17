using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;

namespace PublishServiceConnection
{
    public class PublisherService(HttpClient httpClient)
    {
        public async Task Push(IPushable message)
        {
            if (message is IChatBotNotification chatNotification)
            {
                foreach(var notif in chatNotification.ToChatBotNotifications())
                {
                    var url = Environment.GetEnvironmentVariable("CHATBOTNOTIFICATIONSERVICE_URL") ?? throw new ArgumentNullException("Missing env variable");
                    await PushNotification(notif.Key, notif.Value, url);
                }
            }
            if (message is IMailNotification mailNotification) 
            {
                foreach (var notif in mailNotification.ToMailNotifications())
                {
                    var url = Environment.GetEnvironmentVariable("MAILNOTIFICATIONSERVICE_URL") ?? throw new ArgumentNullException("Missing env variable");
                    await PushNotification(notif.Key, mailNotification.MailTitle, notif.Value, url, mailNotification.Template);
                }
            }
            if (message is ICommonNotification commonNotification)
            {
                foreach (var notif in commonNotification.ToCommonNotifications())
                {
                    var url = Environment.GetEnvironmentVariable("COMMONNOTIFICATIONSERVICE_URL") ?? throw new ArgumentNullException("Missing env variable");
                    await PushNotification(notif.Key, notif.Value, url);
                }
            }
        }

        private async Task PushNotification(int userId, string message, string route)
        {
            HttpResponseMessage response = await httpClient.PostAsync(route + $"/publish?userId={userId}&message={Uri.EscapeDataString(message)}", content: null);
            response.EnsureSuccessStatusCode();
        }

        private async Task PushNotification(string address, string subject, string message, string route, EmailTemplate template)
        {
            var url = route
                + $"/publish?address={Uri.EscapeDataString(address)}"
                + $"&subject={Uri.EscapeDataString(subject)}"
                + $"&message={Uri.EscapeDataString(message)}"
                + $"&template={Uri.EscapeDataString(template.ToString())}";

            HttpResponseMessage response = await httpClient.PostAsync(url, content: null);
            response.EnsureSuccessStatusCode();
        }
    }
}

