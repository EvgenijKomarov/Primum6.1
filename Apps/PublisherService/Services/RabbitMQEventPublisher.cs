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
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public RabbitMQEventPublisher(string connectionString)
        {
            _factory = new ConnectionFactory { Uri = new Uri(connectionString) };
        }

        public async Task Publish(string queueName, string message, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(message, _serializerOptions);
            var body = Encoding.UTF8.GetBytes(json);

            for (int attempt = 0; attempt < _maxRetries; attempt++)
            {
                IConnection? connection = null;
                try
                {
                    connection = await _factory.CreateConnectionAsync(cancellationToken);

                    await using var channel = await connection.CreateChannelAsync();

                    await channel.QueueDeclareAsync(
                        queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null,
                        cancellationToken: cancellationToken
                    );

                    await channel.BasicPublishAsync(
                        exchange: string.Empty,
                        routingKey: queueName,
                        mandatory: true,
                        body: body,
                        cancellationToken: cancellationToken
                    );

                    // Успех — выходим из цикла
                    return;
                }
                catch (Exception) when (attempt < _maxRetries - 1)
                {
                    // Экспоненциальная задержка перед повторной попыткой
                    var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
                    try { await Task.Delay(delay, cancellationToken); }
                    catch (OperationCanceledException) { throw; }
                }
                finally
                {
                    // Гарантированное закрытие соединения
                    if (connection is not null)
                    {
                        await connection.CloseAsync(cancellationToken);
                        await connection.DisposeAsync();
                    }
                }
            }

            // Если все попытки исчерпаны — выбрасываем исключение
            throw new InvalidOperationException($"Failed to publish message to queue '{queueName}' after {_maxRetries} attempts");
        }
    }
}
