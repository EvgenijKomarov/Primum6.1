using Pushables;

namespace Publisher.Services
{
    public interface IPublisher
    {
        Task Publish(IPushable message, CancellationToken cancellationToken = default);
    }
}
