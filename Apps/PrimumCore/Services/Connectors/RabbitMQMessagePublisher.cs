using CoreConnection.Notifications;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace PrimumCore.Services.Connectors
{
    public class RabbitMQMessagePublisher(RabbitMQConnection connectionSingleton, ILogger<RabbitMQMessagePublisher>? logger) : IPublisher
    {
        public async Task PublishAsync<TNotification>(TNotification message) where TNotification : INotification
        {
            var connection = await connectionSingleton.GetConnectionAsync();
            string queueName = typeof(TNotification).Name;

            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: false,
                basicProperties: new BasicProperties(),
                body: body);
            logger?.LogInformation($"Notification {body} successfuly pushed on <{queueName}>");
        }
    }
}
