using Pushables;

namespace Publisher.Services
{
    public interface IPublisher
    {
        Task Publish(string queueName, string message, CancellationToken cancellationToken = default);
    }
}
