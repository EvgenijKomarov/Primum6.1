using CoreDBIterator.Workers;
using CoreDBIteratorTests.Infrastructure;

namespace CoreDBIteratorTests.Unit
{
    public class ExpiredTokenDeleteExecutorTests
    {
        private TestServices _services = null!;

        [SetUp]
        public void SetUp() => _services = new TestServices();

        [TearDown]
        public void TearDown() => _services.Dispose();

        [Test]
        public async Task Action_DeletesOnlyExpiredTokens()
        {
            using (var db = _services.CreateContext())
            {
                var seed = new Seed(db);
                var user = seed.User();
                seed.Token(user, DateTime.UtcNow.AddMinutes(-1), "expired-1");
                seed.Token(user, DateTime.UtcNow.AddDays(-3), "expired-2");
                seed.Token(user, DateTime.UtcNow.AddHours(1), "alive");
            }

            await _services.CreateWorker<ExpiredTokenDeleteExecutor>().Action();

            using var check = _services.CreateContext();
            Assert.That(check.VerificationTokens.Select(t => t.Token), Is.EquivalentTo(new[] { "alive" }));
        }

        [Test]
        public async Task Action_WithoutExpiredTokens_KeepsEverything()
        {
            using (var db = _services.CreateContext())
            {
                var seed = new Seed(db);
                seed.Token(seed.User(), DateTime.UtcNow.AddHours(12), "alive");
            }

            await _services.CreateWorker<ExpiredTokenDeleteExecutor>().Action();

            using var check = _services.CreateContext();
            Assert.That(check.VerificationTokens.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task Action_ReportsNumberOfDeletedTokens()
        {
            using (var db = _services.CreateContext())
            {
                var seed = new Seed(db);
                var user = seed.User();
                seed.Token(user, DateTime.UtcNow.AddMinutes(-5), "expired-1");
                seed.Token(user, DateTime.UtcNow.AddMinutes(-5), "expired-2");
            }

            await _services.CreateWorker<ExpiredTokenDeleteExecutor>().Action();

            Assert.That(_services.Logs.Messages, Has.Some.Contains("Found 2 expired tokens"));
            Assert.That(_services.Logs.Messages, Has.None.StartsWith("No expired tokens found"));
        }

        [Test]
        public async Task Action_DoesNotWriteTokenValuesToLogs()
        {
            const string secret = "confirm-code-7f3a9c";
            using (var db = _services.CreateContext())
            {
                var seed = new Seed(db);
                seed.Token(seed.User(), DateTime.UtcNow.AddMinutes(-5), secret);
            }

            await _services.CreateWorker<ExpiredTokenDeleteExecutor>().Action();

            // Код подтверждения почты — секрет: в логах ему не место
            Assert.That(_services.Logs.Messages, Has.None.Contains(secret));
        }
    }
}
