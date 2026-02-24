using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Publisher.Services
{
    public class RabbitMQEventPublisher : IPublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly int _maxRetries = 3;
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public RabbitMQEventPublisher(string connectionString)
        {
            _factory = new ConnectionFactory { Uri = new Uri(connectionString) };
        }

        public async Task Publish(string queueName, string message, CancellationToken cancellationToken = default)//не понятно работает ли
        {
            IConnection _connection = await _factory.CreateConnectionAsync(cancellationToken);

            var json = JsonSerializer.Serialize(message, _serializerOptions);

            var body = Encoding.UTF8.GetBytes(json);

            for (int attempt = 0; attempt < _maxRetries; attempt++)
            {
                try
                {
                    using var channel = await _connection.CreateChannelAsync();

                    await channel.QueueDeclareAsync(
                        queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    await channel.BasicPublishAsync(
                        exchange: string.Empty,
                        routingKey: queueName,
                        body: body
                    );

                    return;
                }
                catch (Exception) when (attempt < _maxRetries - 1)
                {
                    // exponential backoff with cancellation support
                    var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
                    try { await Task.Delay(delay, cancellationToken); } catch (OperationCanceledException) { throw; }
                }
            }

            await _connection?.CloseAsync(cancellationToken);
            _connection?.DisposeAsync();
        }
    }
}
