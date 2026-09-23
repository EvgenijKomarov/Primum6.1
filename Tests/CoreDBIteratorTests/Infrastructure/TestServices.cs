using Common.Utilities;
using CoreDBModel.Models;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentServiceConnection;
using PublishServiceConnection;
using SharedCoreBusinessLogic;

namespace CoreDBIteratorTests.Infrastructure
{
    /// <summary>
    /// Контейнер для юнит-тестов воркеров: настоящий PrimumContext на InMemory-базе
    /// (с засеянными рангами), настоящие клиенты платёжки и уведомлений поверх HTTP-заглушки.
    /// Как и в проде, каждый scope получает свой контекст.
    /// </summary>
    public sealed class TestServices : IDisposable
    {
        private readonly InMemoryDatabaseRoot _root = new();
        private readonly string _databaseName = Guid.NewGuid().ToString();

        public FakeExternalServices External { get; } = new();
        public ListLoggerProvider Logs { get; } = new();
        public ServiceProvider Provider { get; }

        public TestServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder => builder.AddProvider(Logs));
            services.AddScoped(_ => new DatabaseIterator(CreateContext()));
            services.AddScoped<IQueryable<Lesson>>(sp => sp.GetRequiredService<DatabaseIterator>().Lessons());
            services.AddScoped<EarningCalculationService>();
            services.AddScoped<ConverterToDateTimeService>();
            services.AddScoped<LessonBuilder>();
            services.AddScoped(_ => new PublisherService(External.CreateClient()));
            services.AddScoped(_ => new PaymentServiceClient(External.CreateClient()));
            Provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });

            using var context = CreateContext();
            context.Database.EnsureCreated();
        }

        public static DbContextOptions<PrimumContext> InMemoryOptions(string databaseName, InMemoryDatabaseRoot root) =>
            new DbContextOptionsBuilder<PrimumContext>()
                .UseInMemoryDatabase(databaseName, root)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

        /// <summary>Отдельный контекст для подготовки данных и проверок, не связанный с воркером</summary>
        public PrimumContext CreateContext() => new(InMemoryOptions(_databaseName, _root));

        public T CreateWorker<T>() where T : class => ActivatorUtilities.CreateInstance<T>(Provider);

        public void Dispose()
        {
            Provider.Dispose();
            Logs.Dispose();
        }
    }
}
