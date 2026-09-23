using CoreDBIterator.Workers;
using CoreDBIteratorTests.Infrastructure;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreDBIteratorTests.Unit
{
    public class LessonCreatingExecutorTests
    {
        private TestServices _services = null!;

        [SetUp]
        public void SetUp() => _services = new TestServices();

        [TearDown]
        public void TearDown() => _services.Dispose();

        private AbonementShedule SeedSchedule(
            DateTime lastIteration,
            AbonementStatus status = AbonementStatus.Active,
            int courseFreeLessons = 0,
            DayOfWeek day = DayOfWeek.Wednesday,
            int hour = 15)
        {
            using var db = _services.CreateContext();
            var seed = new Seed(db);
            var course = seed.Course(seed.Teacher(), price: 900m, freeLessons: courseFreeLessons);
            var abonement = seed.Abonement(course, seed.Student(), status);
            return seed.Schedule(abonement, day, hour, lastIteration);
        }

        private Lesson[] LessonsOf(int abonementId)
        {
            using var db = _services.CreateContext();
            return db.Lessons.Where(l => l.AbonementId == abonementId).AsNoTracking().ToArray();
        }

        [Test]
        public async Task Action_ScheduleDueForProlongation_CreatesWaitingLessonOnScheduledSlot()
        {
            var schedule = SeedSchedule(DateTime.UtcNow.AddDays(-8), day: DayOfWeek.Wednesday, hour: 15);

            await _services.CreateWorker<LessonCreatingExecutor>().Action();

            var lesson = LessonsOf(schedule.AbonementId).Single();
            Assert.Multiple(() =>
            {
                Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Waiting));
                Assert.That(lesson.DateTime.DayOfWeek, Is.EqualTo(DayOfWeek.Wednesday));
                Assert.That(lesson.DateTime.Hour, Is.EqualTo(15));
                Assert.That(lesson.DateTime, Is.GreaterThan(DateTime.UtcNow), "урок создаётся в будущем");
                Assert.That(lesson.Price, Is.EqualTo(900m));
            });
        }

        [Test]
        public async Task Action_MovesLastIterationSoTheNextRunDoesNotDuplicateLesson()
        {
            var schedule = SeedSchedule(DateTime.UtcNow.AddDays(-8));
            var worker = _services.CreateWorker<LessonCreatingExecutor>();

            await worker.Action();
            await worker.Action();

            Assert.That(LessonsOf(schedule.AbonementId), Has.Length.EqualTo(1));
            using var db = _services.CreateContext();
            var lastIteration = db.AbonementShedules.Single(s => s.Id == schedule.Id).LastIteration;
            Assert.That(lastIteration, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromMinutes(1)));
        }

        [Test]
        public async Task Action_ScheduleIteratedLessThanWeekAgo_IsSkipped()
        {
            var schedule = SeedSchedule(DateTime.UtcNow.AddDays(-3));

            await _services.CreateWorker<LessonCreatingExecutor>().Action();

            Assert.That(LessonsOf(schedule.AbonementId), Is.Empty);
        }

        [Test]
        public async Task Action_DeletedAbonement_GetsNoLessonButIterationMoves()
        {
            var schedule = SeedSchedule(DateTime.UtcNow.AddDays(-8), status: AbonementStatus.Deleted);

            await _services.CreateWorker<LessonCreatingExecutor>().Action();

            Assert.That(LessonsOf(schedule.AbonementId), Is.Empty);
            using var db = _services.CreateContext();
            var lastIteration = db.AbonementShedules.Single(s => s.Id == schedule.Id).LastIteration;
            Assert.That(lastIteration, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromMinutes(1)),
                "иначе удалённый абонемент перебирается каждые 10 минут");
        }

        [Test]
        public async Task Action_CourseWithUnusedFreeLesson_CreatesFreeLesson()
        {
            var schedule = SeedSchedule(DateTime.UtcNow.AddDays(-8), courseFreeLessons: 1);

            await _services.CreateWorker<LessonCreatingExecutor>().Action();

            Assert.That(LessonsOf(schedule.AbonementId).Single().Price, Is.EqualTo(0m));
        }

        [Test]
        public void Action_NothingDue_DoesNotFail()
        {
            Assert.DoesNotThrowAsync(() => _services.CreateWorker<LessonCreatingExecutor>().Action());
        }
    }
}
