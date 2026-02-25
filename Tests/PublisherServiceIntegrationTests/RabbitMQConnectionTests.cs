using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Publisher.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Shouldly;
using System.Text;
using System.Text.Json;
using Testcontainers.RabbitMq;

namespace PublisherServiceIntegrationTests
{
    public class MassTransitRabbitMQTests // run docker firstly!
    {
        [SetUpFixture]
        public class RabbitMQTestSetup
        {
            public static RabbitMqContainer? RabbitMqContainer { get; private set; }
            public static string? ConnectionString { get; private set; }

            [OneTimeSetUp]
            public async Task RunBeforeAnyTests()
            {
                RabbitMqContainer = new RabbitMqBuilder()
                    .WithUsername("guest")
                    .WithPassword("guest")
                    .WithPortBinding(5672, false)
                    .Build();

                await RabbitMqContainer.StartAsync();
                ConnectionString = RabbitMqContainer.GetConnectionString();
            }

            [OneTimeTearDown]
            public async Task RunAfterAnyTests()
            {
                if (RabbitMqContainer != null)
                {
                    await RabbitMqContainer.DisposeAsync();
                }
            }
        }

        // Простой тестовый контракт — record удобен для сравнения
        public record TestMessage(string Content, Guid CorrelationId);

        [TestFixture]
        public class RabbitMQEventPublisherTests
        {
            private IBus? _bus;
            private TestConsumer? _consumer;
            private readonly List<TestMessage> _receivedMessages = new();

            [SetUp]
            public async Task SetUp()
            {
                _receivedMessages.Clear();
                _consumer = new TestConsumer(_receivedMessages);

                _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(RabbitMQTestSetup.ConnectionString!);

                    // Подписываем тестового потребителя на очередь для TestMessage
                    cfg.ReceiveEndpoint("test-consumer-queue", e =>
                    {
                        e.ConfigureConsumer<TestConsumer>(null);
                    });
                });
            }

            [TearDown]
            public async Task TearDown()
            {
                
            }

            /// <summary>
            /// Базовый тест: сообщение публикуется и корректно десериализуется
            /// </summary>
            [Test]
            public async Task Publish_MessageSuccessfullySent_AndDeserialized()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(_bus!, NullLogger<RabbitMQEventPublisher>.Instance);
                var testMessage = new TestMessage("Hello, MassTransit!", Guid.NewGuid());

                // Act
                await publisher.Publish(testMessage, CancellationToken.None);

                // Assert
                var received = await WaitForMessageAsync(TimeSpan.FromSeconds(5));

                Assert.That(received, Is.Not.Null, "Message should be received");
                Assert.That(received!.Content, Is.EqualTo(testMessage.Content));
                Assert.That(received.CorrelationId, Is.EqualTo(testMessage.CorrelationId));
            }

            /// <summary>
            /// Проверяем, что CancellationToken корректно пробрасывается в MassTransit
            /// </summary>
            [Test]
            public async Task Publish_CancellationTokenRespected_WhenCancelled()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(_bus!, NullLogger<RabbitMQEventPublisher>.Instance);
                var testMessage = new TestMessage("test", Guid.NewGuid());
                using var cts = new CancellationTokenSource();
                cts.Cancel();

                // Act & Assert - MassTransit пробросит OperationCanceledException
                Assert.ThrowsAsync<OperationCanceledException>(async () =>
                {
                    await publisher.Publish(testMessage, cts.Token);
                });
            }

            /// <summary>
            /// Проверяем порядок доставки нескольких сообщений
            /// </summary>
            [Test]
            public async Task Publish_MultipleMessages_ReceivedInOrder()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(_bus!, NullLogger<RabbitMQEventPublisher>.Instance);
                var messages = new[]
                {
                new TestMessage("First", Guid.NewGuid()),
                new TestMessage("Second", Guid.NewGuid()),
                new TestMessage("Third", Guid.NewGuid())
            };

                // Act
                foreach (var msg in messages)
                {
                    await publisher.Publish(msg, CancellationToken.None);
                }

                // Wait for all messages
                var received = await WaitForMessagesAsync(3, TimeSpan.FromSeconds(10));

                // Assert
                Assert.That(received, Has.Count.EqualTo(3));
                CollectionAssert.AreEqual(
                    messages.Select(m => m.Content),
                    received.Select(r => r.Content),
                    "Messages should be received in order");
            }

            /// <summary>
            /// Проверяем, что спецсимволы и Unicode сохраняются при JSON-сериализации MassTransit
            /// </summary>
            [Test]
            public async Task Publish_SpecialCharactersInMessage_PreservedCorrectly()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(_bus!, NullLogger<RabbitMQEventPublisher>.Instance);
                var testMessage = new TestMessage("Hello \"world\" with 🚀 and \\n newlines", Guid.NewGuid());

                // Act
                await publisher.Publish(testMessage, CancellationToken.None);
                var received = await WaitForMessageAsync(TimeSpan.FromSeconds(5));

                // Assert
                Assert.That(received, Is.Not.Null);
                Assert.That(received!.Content, Is.EqualTo(testMessage.Content));
            }

            /// <summary>
            /// [Опционально] Проверяем "сырой" вид сообщения в RabbitMQ через RabbitMQ.Client
            /// Это помогает убедиться, как именно MassTransit сериализует сообщение
            /// </summary>
            [Test]
            public async Task Publish_MessageStructure_VerifyRawJson()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(_bus!, NullLogger<RabbitMQEventPublisher>.Instance);
                var testMessage = new TestMessage("raw check", Guid.Parse("11111111-1111-1111-1111-111111111111"));

                // Создаём отдельное соединение для "прослушки" сырых сообщений
                using var connection = await new ConnectionFactory
                {
                    Uri = new Uri(RabbitMQTestSetup.ConnectionString!)
                }.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                // Объявляем временную очередь для перехвата
                var tempQueue = $"raw-inspect-{Guid.NewGuid()}";
                await channel.QueueDeclareAsync(tempQueue, durable: false, exclusive: true, autoDelete: true);
                await channel.QueueBindAsync(tempQueue, "test-consumer-queue", string.Empty); // bind к exchange

                // Act
                await publisher.Publish(testMessage, CancellationToken.None);

                // Consume raw message
                var result = await channel.BasicGetAsync(tempQueue, autoAck: true);
                Assert.That(result, Is.Not.Null, "Raw message should be captured");

                var rawJson = Encoding.UTF8.GetString(result!.Body.ToArray());

                // Assert - проверяем, что JSON содержит ожидаемые поля
                // MassTransit по умолчанию добавляет метаданные, но Content должен быть внутри
                Assert.That(rawJson, Does.Contain("Content"));
                Assert.That(rawJson, Does.Contain("raw check"));
                Assert.That(rawJson, Does.Contain("11111111-1111-1111-1111-111111111111"));
            }

            // ─────────────────────────────────────────────────────────────
            // Вспомогательные методы
            // ─────────────────────────────────────────────────────────────

            private async Task<TestMessage?> WaitForMessageAsync(TimeSpan timeout)
            {
                var start = DateTime.UtcNow;
                while (DateTime.UtcNow - start < timeout)
                {
                    lock (_receivedMessages)
                    {
                        if (_receivedMessages.Count > 0)
                            return _receivedMessages[0];
                    }
                    await Task.Delay(50);
                }
                return null;
            }

            private async Task<List<TestMessage>> WaitForMessagesAsync(int count, TimeSpan timeout)
            {
                var start = DateTime.UtcNow;
                while (DateTime.UtcNow - start < timeout)
                {
                    lock (_receivedMessages)
                    {
                        if (_receivedMessages.Count >= count)
                            return new List<TestMessage>(_receivedMessages);
                    }
                    await Task.Delay(50);
                }
                lock (_receivedMessages)
                    return new List<TestMessage>(_receivedMessages);
            }

            // ─────────────────────────────────────────────────────────────
            // Тестовый потребитель
            // ─────────────────────────────────────────────────────────────

            private class TestConsumer : IConsumer<TestMessage>
            {
                private readonly List<TestMessage> _storage;

                public TestConsumer(List<TestMessage> storage) => _storage = storage;

                public Task Consume(ConsumeContext<TestMessage> context)
                {
                    lock (_storage)
                    {
                        _storage.Add(context.Message);
                    }
                    return Task.CompletedTask;
                }
            }
        }
    }
}
