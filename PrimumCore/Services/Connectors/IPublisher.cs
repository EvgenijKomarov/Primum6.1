namespace PrimumCore.Services.Connectors
{
    public interface IPublisher
    {
        Task PublishAsync<TNotification>(TNotification message);
    }
}
