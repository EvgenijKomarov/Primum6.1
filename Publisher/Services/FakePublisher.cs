using Pushables;
using System.Text.Json;

namespace Publisher.Services
{
    public class FakePublisher(ILogger<FakePublisher> logger) : IPublisher
    {
        public Task Publish(IPushable message, CancellationToken cancellationToken = default)
        {
            // Получаем реальный тип сообщения
            var messageType = message.GetType();

            // Сериализуем на основе реального типа, чтобы увидеть все свойства
            var json = JsonSerializer.Serialize(message, messageType, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            logger.LogWarning("Fake Publish [{Type}]: {Json}", messageType.Name, json);

            return Task.CompletedTask;
        }
    }
}
