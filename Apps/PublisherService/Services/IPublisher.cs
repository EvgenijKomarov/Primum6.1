using PublishServiceConnection;

namespace Publisher.Services
{
    public interface IPublisher
    {
        Task Publish<T>(T message, CancellationToken cancellationToken = default) where T: class;
    }
}
