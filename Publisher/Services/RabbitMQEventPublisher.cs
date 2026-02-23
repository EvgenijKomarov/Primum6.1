using MassTransit;
using Pushables;

namespace Publisher.Services
{
    public class RabbitMQEventPublisher(IPublishEndpoint _publishEndpoint) : IPublisher
    {
        public async Task Publish(IPushable message, CancellationToken cancellationToken = default)
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
