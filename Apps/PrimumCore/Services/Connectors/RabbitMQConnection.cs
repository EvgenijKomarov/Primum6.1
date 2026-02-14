using Microsoft.Extensions.Options;
using PrimumCore.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PrimumCore.Services.Connectors
{
    public class RabbitMQConnection : IAsyncDisposable
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly ILogger<RabbitMQConnection> _logger;
        private readonly RabbitMQSettings _settings;

        private IConnection? _connection;
        private bool _disposed;

        public RabbitMQConnection(
            IOptions<RabbitMQSettings> options,
            ILogger<RabbitMQConnection> logger)
        {
            _settings = options.Value;
            _logger = logger;

            _ = InitializeConnectionAsync(); // Запуск инициализации
        }

        /// <summary>
        /// Получить активное соединение (с автоматическим восстановлением)
        /// </summary>
        public async Task<IConnection> GetConnectionAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(RabbitMQConnection));

            if (_connection?.IsOpen == true)
                return _connection;

            // Ждём завершения инициализации
            await _semaphore.WaitAsync();
            try
            {
                if (_connection?.IsOpen == true)
                    return _connection;

                _logger.LogWarning("Connection is not open, reinitializing...");
                await InitializeConnectionAsync();
                return _connection!;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Инициализация или восстановление соединения
        /// </summary>
        private async Task InitializeConnectionAsync()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _settings.HostName,
                    Port = _settings.Port,
                    UserName = _settings.UserName,
                    Password = _settings.Password,
                    VirtualHost = _settings.VirtualHost,
                    AutomaticRecoveryEnabled = true,
                };

                _connection?.Dispose(); // Закрыть старое соединение

                _connection = await factory.CreateConnectionAsync();

                _logger.LogInformation(
                    "RabbitMQ connected to {HostName}:{Port}/{VirtualHost}",
                    _settings.HostName,
                    _settings.Port,
                    _settings.VirtualHost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize RabbitMQ connection");
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
                return;

            _disposed = true;

            try
            {
                _logger.LogInformation("Disposing RabbitMQ connection");

                if (_connection != null && _connection.IsOpen)
                {
                    await _connection.CloseAsync();
                    _connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during RabbitMQ connection disposal");
            }
            finally
            {
                _semaphore.Dispose();
            }
        }
    }
}
