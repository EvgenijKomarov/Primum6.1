using CoreConnection.Notifications;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace PrimumCore.Services.Connectors
{
    public class FakePublisher(ILogger<FakePublisher>? logger): IPublisher
    {
        public async Task PublishAsync<TNotification>(TNotification message) where TNotification : INotification
        {
            string queueName = typeof(TNotification).Name;
            string jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            logger?.LogInformation($"Notification {queueName} ({jsonMessage}) successfuly fake pushed on <{queueName}>");
        }
    }
}
