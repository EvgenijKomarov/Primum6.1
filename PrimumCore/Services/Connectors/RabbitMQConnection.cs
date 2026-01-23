using Microsoft.Extensions.Options;
using PrimumCore.Options;
using RabbitMQ.Client;

namespace PrimumCore.Services.Connectors
{
    public class RabbitMQConnection
    {
        private readonly Lazy<Task<IConnection>> _connectionLazy;

        public RabbitMQConnection(IOptions<RabbitMQSettings> options)
        {
            _connectionLazy = new Lazy<Task<IConnection>>(async () =>
            {
                var settings = options.Value;
                var factory = new ConnectionFactory
                {
                    HostName = settings.HostName,
                    Port = settings.Port,
                    UserName = settings.UserName,
                    Password = settings.Password,
                    VirtualHost = settings.VirtualHost,
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                };

                return await factory.CreateConnectionAsync();
            });
        }

        public Task<IConnection> GetConnectionAsync() => _connectionLazy.Value;
    }
}
