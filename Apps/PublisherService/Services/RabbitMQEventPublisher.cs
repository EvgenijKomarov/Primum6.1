using MassTransit;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Publisher.Services
{
    public class RabbitMQEventPublisher(IBus bus, ILogger<RabbitMQEventPublisher> logger) : IPublisher
    {
        public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                // MassTransit автоматически:
                // 1. Сериализует сообщение (JSON по умолчанию)
                // 2. Создаёт очередь с именем по типу T (если не настроено иначе)
                // 3. Обрабатывает reconnect и retry
                await bus.Publish(message, cancellationToken);

                logger.LogInformation("Published {MessageType} to queue", typeof(T).Name);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish {MessageType}", typeof(T).Name);
                throw;
            }
        }
    }
}
