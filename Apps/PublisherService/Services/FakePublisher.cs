using PublishServiceConnection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Publisher.Services
{
    public class FakePublisher(ILogger<FakePublisher> logger) : IPublisher
    {
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        {

            logger.LogWarning("Fake published on [{queueName}]: {messageJson}", typeof(T).Name, JsonSerializer.Serialize(message, options));

            return Task.CompletedTask;
        }
    }
}
