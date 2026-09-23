using System.Diagnostics;
using System.Net;
using Common.Utilities;
using CoreDBIterator.Extensions;
using CoreDBIterator.Workers;
using CoreDBIteratorTests.Infrastructure;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentServiceConnection;
using PublishServiceConnection;
using SharedCoreBusinessLogic;

namespace CoreDBIteratorTests.Integration
{
    /// <summary>
    /// Автотесты сервиса целиком: хост собирается той же регистрацией, что и в Program.cs
    /// (AddCoreDbIteratorServices), подменяются только база (InMemory) и внешние HTTP-сервисы.
    /// </summary>
    [NonParallelizable]
    public class CoreDbIteratorHostTests
    {
        private static readonly TimeSpan WaitLimit = TimeSpan.FromSeconds(15);

        private readonly InMemoryDatabaseRoot _root = new();
        private string _databaseName = null!;
        private FakeExternalServices _external = null!;
        private ListLoggerProvider _logs = null!;

        [SetUp]
        public void SetUp()
        {
            _databaseName = Guid.NewGuid().ToString();
            _external = new FakeExternalServices();
            _logs = new ListLoggerProvider();
            using var db = CreateContext();
            db.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown() => _logs.Dispose();

        private PrimumContext CreateContext() => new(TestServices.InMemoryOptions(_databaseName, _root));

        private IHost BuildHost() => new HostBuilder()
            .ConfigureLogging(logging => logging.ClearProviders().AddProvider(_logs))
            .ConfigureServices(services =>
            {
                services.AddCoreDbIteratorServices();

                services.RemoveAll<DatabaseIterator>();
                services.AddScoped(_ => new DatabaseIterator(CreateContext()));
                services.ConfigureHttpClientDefaults(http =>
                    http.ConfigurePrimaryHttpMessageHandler(() => _external.CreateHandler()));
            })
            .UseDefaultServiceProvider(options =>
            {
                options.ValidateOnBuild = true;
                options.ValidateScopes = true;
            })
            .Build();

        private static async Task WaitUntil(Func<bool> condition, string description)
        {
            var timer = Stopwatch.StartNew();
            while (!condition())
            {
                if (timer.Elapsed > WaitLimit) Assert.Fail($"За {WaitLimit.TotalSeconds} с не дождались: {description}");
                await Task.Delay(100);
            }
        }

        [Test]
        public void ProductionRegistration_ResolvesAllWorkersAndTheirDependencies()
        {
            using var host = BuildHost();

            var workers = host.Services.GetServices<IHostedService>().Select(s => s.GetType()).ToArray();
            Assert.That(workers, Is.SupersetOf(new[]
            {
                typeof(LessonCreatingExecutor),
                typeof(LessonWarningExecutor),
                typeof(LessonIteratorExecutor),
                typeof(ExpiredTokenDeleteExecutor),
                typeof(TeacherProfileRefreshExecutor),
            }));

            // Воркеры достают зависимости из scope вручную, ValidateOnBuild этого не видит
            using var scope = host.Services.CreateScope();
            Assert.Multiple(() =>
            {
                Assert.That(scope.ServiceProvider.GetService<DatabaseIterator>(), Is.Not.Null);
                Assert.That(scope.ServiceProvider.GetService<PublisherService>(), Is.Not.Null);
                Assert.That(scope.ServiceProvider.GetService<PaymentServiceClient>(), Is.Not.Null);
                Assert.That(scope.ServiceProvider.GetService<EarningCalculationService>(), Is.Not.Null);
                Assert.That(scope.ServiceProvider.GetService<LessonBuilder>(), Is.Not.Null);
            });
        }

        [Test]
        public async Task Host_RunsAllWorkersOnStartAndStopsCleanly()
        {
            int scheduleAbonementId, upcomingLessonId, dueLessonId;
            using (var db = CreateContext())
            {
                // Сценарий создаётся первым: он рассчитывает на раскладку id с чистой базы
                var scenario = LessonScenario.Create(db);
                var seed = new Seed(db);
                seed.Token(seed.User(), DateTime.UtcNow.AddHours(-1), "expired");

                var abonement = db.Abonements.Single(a => a.Id == scenario.AbonementId);
                upcomingLessonId = seed.Lesson(abonement, DateTime.UtcNow.AddHours(5)).Id;
                dueLessonId = seed.Lesson(abonement, DateTime.UtcNow.AddMinutes(15), LessonStatus.Warned).Id;
                seed.Schedule(abonement, DayOfWeek.Friday, 12, DateTime.UtcNow.AddDays(-8));
                scheduleAbonementId = abonement.Id;

                _external.ReadyTeachers[scenario.TeacherUserId] = true;
                _external.Balances[scenario.StudentUserId] = 10_000m;
            }

            using var host = BuildHost();
            await host.StartAsync();

            await WaitUntil(() => { using var db = CreateContext(); return !db.VerificationTokens.Any(); },
                "удаление просроченного токена");
            await WaitUntil(() => { using var db = CreateContext(); return db.Lessons.Single(l => l.Id == upcomingLessonId).Status == LessonStatus.Warned; },
                "предупреждение о занятии через 5 часов");
            await WaitUntil(() => { using var db = CreateContext(); return db.Lessons.Single(l => l.Id == dueLessonId).Status == LessonStatus.Happened; },
                "проведение оплаченного занятия через 15 минут");
            await WaitUntil(() => { using var db = CreateContext(); return db.Lessons.Count(l => l.AbonementId == scheduleAbonementId && l.DateTime > DateTime.UtcNow.AddDays(3)) == 1; },
                "создание занятия на следующую неделю по расписанию");

            var timer = Stopwatch.StartNew();
            await host.StopAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            Assert.That(timer.Elapsed, Is.LessThan(TimeSpan.FromSeconds(5)), "воркеры должны сразу выходить из ожидания по токену остановки");
        }

        [Test]
        public async Task Host_KeepsRunningWhenOneWorkerIterationFails()
        {
            // Платёжный сервис недоступен: LessonWarningExecutor не может узнать баланс ученика
            _external.PaymentServiceStatus = HttpStatusCode.ServiceUnavailable;
            using (var db = CreateContext())
            {
                var seed = new Seed(db);
                var scenario = LessonScenario.Create(db);
                seed.Lesson(db.Abonements.Single(a => a.Id == scenario.AbonementId), DateTime.UtcNow.AddHours(5));
                seed.Token(seed.User(), DateTime.UtcNow.AddHours(-1), "expired");
            }

            using var host = BuildHost();
            var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
            await host.StartAsync();

            await WaitUntil(() => { using var db = CreateContext(); return !db.VerificationTokens.Any(); },
                "работа остальных воркеров");
            await Task.Delay(TimeSpan.FromSeconds(1));

            Assert.That(lifetime.ApplicationStopping.IsCancellationRequested, Is.False,
                "сбой одной итерации одного воркера не должен останавливать весь сервис");
            Assert.That(_logs.Messages, Has.Some.Contains(nameof(LessonWarningExecutor)),
                "ошибка итерации должна попадать в лог");

            await host.StopAsync();
        }
    }
}
