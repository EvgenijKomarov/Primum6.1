using CoreDBIterator.Workers;
using CoreDBIteratorTests.Infrastructure;
using CoreDBModel.Models.Enums;

namespace CoreDBIteratorTests.Unit
{
    public class LessonWarningExecutorTests
    {
        private TestServices _services = null!;
        private LessonScenario _scenario = null!;

        [SetUp]
        public void SetUp()
        {
            _services = new TestServices();
            using var db = _services.CreateContext();
            _scenario = LessonScenario.Create(db);
        }

        [TearDown]
        public void TearDown() => _services.Dispose();

        private int AddLesson(DateTime at, LessonStatus status = LessonStatus.Waiting)
        {
            using var db = _services.CreateContext();
            var abonement = db.Abonements.Single(a => a.Id == _scenario.AbonementId);
            return new Seed(db).Lesson(abonement, at, status).Id;
        }

        private LessonStatus StatusOf(int lessonId)
        {
            using var db = _services.CreateContext();
            return db.Lessons.Single(l => l.Id == lessonId).Status;
        }

        [Test]
        public async Task Action_WaitingLessonWithinDay_BecomesWarned()
        {
            var lessonId = AddLesson(DateTime.UtcNow.AddHours(5));

            await _services.CreateWorker<LessonWarningExecutor>().Action();

            Assert.That(StatusOf(lessonId), Is.EqualTo(LessonStatus.Warned));
        }

        [Test]
        public async Task Action_IgnoresLessonsLaterThanDayAndNotWaiting()
        {
            var later = AddLesson(DateTime.UtcNow.AddDays(3));
            var cancelled = AddLesson(DateTime.UtcNow.AddHours(2), LessonStatus.Cancelled);
            var happened = AddLesson(DateTime.UtcNow.AddHours(-2), LessonStatus.Happened);

            await _services.CreateWorker<LessonWarningExecutor>().Action();

            Assert.Multiple(() =>
            {
                Assert.That(StatusOf(later), Is.EqualTo(LessonStatus.Waiting));
                Assert.That(StatusOf(cancelled), Is.EqualTo(LessonStatus.Cancelled));
                Assert.That(StatusOf(happened), Is.EqualTo(LessonStatus.Happened));
            });
        }

        [Test]
        public async Task Action_ChecksTeacherReadinessByTeacherUserId()
        {
            AddLesson(DateTime.UtcNow.AddHours(5));

            await _services.CreateWorker<LessonWarningExecutor>().Action();

            // Платёжный сервис знает преподавателя по id пользователя (так же спрашивает LessonIteratorExecutor)
            var readinessChecks = _services.External.PaymentRequests("/is-teacher-ready/").Select(u => u.AbsolutePath);
            Assert.That(readinessChecks, Is.EquivalentTo(new[] { $"/is-teacher-ready/{_scenario.TeacherUserId}" }));
        }
    }
}
