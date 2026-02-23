using Pushables;
using System.Text.Json;

namespace Publisher.Services
{
    public class FakePublisher(ILogger<FakePublisher> logger) : IPublisher
    {
        public Task Publish(string queueName, string messageJson, CancellationToken cancellationToken = default)
        {

            logger.LogWarning("Fake published on [{queueName}]: {messageJson}", queueName, messageJson);

            return Task.CompletedTask;
        }
    }
}
