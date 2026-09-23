using PublishServiceConnection.Abstractions;
using PublishServiceConnection.Enums;
using System.Net.Http.Json;
using System.Text.Json;

namespace PublishServiceConnection
{
    public class PublisherService(HttpClient httpClient)
    {
        // camelCase по умолчанию — совпадает с полями userId/message/address/subject/template в Pydantic
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public async Task Push(IPushable message)
        {
            if (message is IChatBotNotification chatNotification)
            {
                var url = Environment.GetEnvironmentVariable("CHATBOTNOTIFICATIONSERVICE_URL")
                          ?? throw new ArgumentNullException("Missing env variable");
                foreach (var notif in chatNotification.ToChatBotNotifications())
                {
                    await PushNotification(notif.Key, notif.Value, url);
                }
            }

            if (message is IMailNotification mailNotification)
            {
                var url = Environment.GetEnvironmentVariable("MAILNOTIFICATIONSERVICE_URL")
                          ?? throw new ArgumentNullException("Missing env variable");
                foreach (var notif in mailNotification.ToMailNotifications())
                {
                    await PushNotification(notif.Key, mailNotification.MailTitle, notif.Value, url, mailNotification.Template);
                }
            }

            if (message is ICommonNotification commonNotification)
            {
                var url = Environment.GetEnvironmentVariable("COMMONNOTIFICATIONSERVICE_URL")
                          ?? throw new ArgumentNullException("Missing env variable");
                foreach (var notif in commonNotification.ToCommonNotifications())
                {
                    await PushNotification(notif.Key, notif.Value, url);
                }
            }
        }

        private async Task PushNotification(int userId, string message, string route)
        {
            var payload = new { userId, message };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(route + "/publish", payload, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        private async Task PushNotification(string address, string subject, string message, string route, EmailTemplate template)
        {
            var payload = new { address, subject, message, template = template.ToString() };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(route + "/publish", payload, JsonOptions);
            response.EnsureSuccessStatusCode();
        }
    }
}

