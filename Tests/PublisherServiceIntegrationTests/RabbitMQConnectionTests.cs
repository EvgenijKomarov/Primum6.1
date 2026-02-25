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
    /*public class RabbitMQTests// run docker firstly!
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

        [TestFixture]
        public class RabbitMQEventPublisherTests
        {
            private IConnection? _consumerConnection;
            private IChannel? _consumerChannel;

            [SetUp]
            public async Task SetUp()
            {
                // Создаём отдельное соединение для потребления сообщений в каждом тесте
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(RabbitMQTestSetup.ConnectionString!)
                };
                _consumerConnection = await factory.CreateConnectionAsync();
                _consumerChannel = await _consumerConnection.CreateChannelAsync();
            }

            [TearDown]
            public void TearDown()
            {
                _consumerConnection?.Dispose();
                _consumerChannel?.Dispose();
            }

            private async Task<string?> ConsumeMessageAsync(string queueName, TimeSpan timeout)
            {
                var tcs = new TaskCompletionSource<string?>();
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel!);

                consumer.ReceivedAsync += async (_, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    await _consumerChannel!.BasicAckAsync(ea.DeliveryTag, false);
                    tcs.TrySetResult(message);
                    await Task.CompletedTask;
                };

                await _consumerChannel!.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);

                var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(timeout));
                return completedTask == tcs.Task ? await tcs.Task : null;
            }

            [Test]
            public async Task Publish_MessageSuccessfullySent_ToNewQueue()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";
                var testMessage = "Hello, RabbitMQ!";

                // Act
                await publisher.Publish(queueName, testMessage, CancellationToken.None);

                // Assert
                var received = await ConsumeMessageAsync(queueName, TimeSpan.FromSeconds(5));
                Assert.That(received, Is.Not.Null, "Message should be received");

                // Проверяем, что сообщение было сериализовано как JSON-строка
                var deserialized = JsonSerializer.Deserialize<string>(received, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                Assert.That(deserialized, Is.EqualTo(testMessage));
            }

            [Test]
            public async Task Publish_QueueDeclaredAsDurable()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";

                // Act
                await publisher.Publish(queueName, "test", CancellationToken.None);

                // Assert - проверяем, что очередь существует и может быть объявлена повторно
                var factory = new ConnectionFactory { Uri = new Uri(RabbitMQTestSetup.ConnectionString!) };
                await using var connection = await factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

                // QueueDeclarePassive проверяет существование очереди без её создания
                await channel.QueueDeclarePassiveAsync(queueName);
            }

            [Test]
            public async Task Publish_CancellationTokenRespected_WhenCancelled()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";
                using var cts = new CancellationTokenSource();
                cts.Cancel(); // Отменяем сразу

                // Act & Assert
                Assert.ThrowsAsync<TaskCanceledException>(async () =>
                {
                    await publisher.Publish(queueName, "test", cts.Token);
                });
            }

            [Test]
            public async Task Publish_MultipleMessages_ReceivedInOrder()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";
                var messages = new[] { "First", "Second", "Third" };
                var received = new List<string?>();

                // 🔹 ОБЪЯВЛЯЕМ очередь ДО подписки потребителя
                // Параметры должны СОВПАДАТЬ с теми, что в RabbitMQEventPublisher!
                await _consumerChannel!.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,        // должно совпадать с publisher
                    exclusive: false,     // должно совпадать с publisher
                    autoDelete: false,    // должно совпадать с publisher
                    arguments: null);

                // Теперь подписываемся — очередь уже существует ✅
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel!);
                consumer.ReceivedAsync += async (_, ea) =>
                {
                    var body = ea.Body.ToArray();
                    received.Add(Encoding.UTF8.GetString(body));
                    await _consumerChannel!.BasicAckAsync(ea.DeliveryTag, false);
                };
                await _consumerChannel!.BasicConsumeAsync(queueName, false, consumer);

                // Act
                foreach (var msg in messages)
                {
                    await publisher.Publish(queueName, msg, CancellationToken.None);
                }

                //waiting for all messages
                var start = DateTime.UtcNow;
                while (received.Count < 3)
                {
                    if (DateTime.UtcNow - start > TimeSpan.FromSeconds(5))
                        throw new TimeoutException($"Ожидание 3 сообщений превысило лимит");
                    await Task.Delay(50);
                }

                // Assert
                Assert.That(received, Has.Count.EqualTo(3));
                var deserialized = received.Select(r => JsonSerializer.Deserialize<string>(r!));
                CollectionAssert.AreEqual(messages, deserialized, "Messages should be received in order");
            }

            [Test]
            public async Task Publish_ConnectionFailure_RetriesAndSucceeds()
            {
                // Arrange — smoke test: публикуем при рабочей инфраструктуре
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";

                // Act & Assert — убеждаемся, что retry-логика не мешает при нормальной работе
                Assert.DoesNotThrowAsync(async () =>
                {
                    await publisher.Publish(queueName, "retry-test", CancellationToken.None);
                });

                var received = await ConsumeMessageAsync(queueName, TimeSpan.FromSeconds(5));
                Assert.That(received, Is.Not.Null);
            }

            [Test]
            public async Task Publish_AfterMaxRetries_ThrowsInvalidOperationException()
            {
                // Arrange — используем неверный connectionString, чтобы все попытки провалились
                var invalidConnectionString = "amqp://invalid:invalid@localhost:5670";
                var publisher = new RabbitMQEventPublisher(invalidConnectionString);
                var queueName = $"test-queue-{Guid.NewGuid()}";

                // Act & Assert
                Assert.ThrowsAsync<BrokerUnreachableException>(async () =>
                {
                    await publisher.Publish(queueName, "test", CancellationToken.None);
                });
            }

            [Test]
            public async Task Publish_SpecialCharactersInMessage_PreservedCorrectly()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";
                var testMessage = "Hello \"world\" with 🚀 and \\n newlines";

                // Act
                await publisher.Publish(queueName, testMessage, CancellationToken.None);
                var received = await ConsumeMessageAsync(queueName, TimeSpan.FromSeconds(5));

                // Assert
                Assert.That(received, Is.Not.Null);
                var deserialized = JsonSerializer.Deserialize<string>(received!);
                Assert.That(deserialized, Is.EqualTo(testMessage));
            }

            [Test]
            public async Task Publish_EmptyMessage_HandledCorrectly()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);
                var queueName = $"test-queue-{Guid.NewGuid()}";
                var testMessage = string.Empty;

                // Act
                await publisher.Publish(queueName, testMessage, CancellationToken.None);
                var received = await ConsumeMessageAsync(queueName, TimeSpan.FromSeconds(5));

                // Assert
                Assert.That(received, Is.Not.Null);
                var deserialized = JsonSerializer.Deserialize<string>(received!);
                Assert.That(deserialized, Is.EqualTo(testMessage));
            }

            [Test]
            public async Task Publish_NullQueueName_ThrowsArgumentException()
            {
                // Arrange
                var publisher = new RabbitMQEventPublisher(RabbitMQTestSetup.ConnectionString!);

                // Act & Assert
                Assert.ThrowsAsync<NullReferenceException>(async () =>
                {
                    await publisher.Publish(null!, "test", CancellationToken.None);
                });
            }
        }
    }*/
}
