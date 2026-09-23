using CoreDBIterator.Workers;
using CoreDBIteratorTests.Infrastructure;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;

namespace CoreDBIteratorTests.Unit
{
    public class TeacherProfileRefreshExecutorTests
    {
        private TestServices _services = null!;

        [SetUp]
        public void SetUp() => _services = new TestServices();

        [TearDown]
        public void TearDown() => _services.Dispose();

        /// <summary>
        /// Преподаватель с <paramref name="abonements"/> абонементами, прошедшими бесплатный период;
        /// в первых <paramref name="converted"/> из них ученик оплатил и провёл занятие.
        /// </summary>
        private int SeedTeacher(int abonements, int converted, bool isBanned = false, float? initialIndex = null)
        {
            using var db = _services.CreateContext();
            var seed = new Seed(db);
            var teacher = seed.Teacher(seed.User("Teacher"), isBanned);
            teacher.ConvertionIndex = initialIndex;
            var course = seed.Course(teacher, 1000m);

            for (var i = 0; i < abonements; i++)
            {
                var abonement = seed.Abonement(course, seed.Student(), freeLessons: 1);
                seed.Lesson(abonement, DateTime.UtcNow.AddDays(-14), LessonStatus.Happened, price: 0m);
                if (i < converted)
                    seed.Lesson(abonement, DateTime.UtcNow.AddDays(-7), LessonStatus.Happened, price: 1000m);
            }

            db.SaveChanges();
            return teacher.Id;
        }

        private float? IndexOf(int teacherId)
        {
            using var db = _services.CreateContext();
            return db.TeacherProfiles.Single(t => t.Id == teacherId).ConvertionIndex;
        }

        private Task Run() => _services.CreateWorker<TeacherProfileRefreshExecutor>().Action();

        [Test]
        public async Task Action_TenCompletedAbonements_IndexIsShareOfPaidOnes()
        {
            var teacherId = SeedTeacher(abonements: 10, converted: 4);

            await Run();

            Assert.That(IndexOf(teacherId), Is.EqualTo(0.4f).Within(0.0001f));
        }

        [Test]
        public async Task Action_FewerThanTenCompletedAbonements_IndexIsUndefined()
        {
            var teacherId = SeedTeacher(abonements: 9, converted: 9, initialIndex: 0.5f);

            await Run();

            Assert.That(IndexOf(teacherId), Is.Null);
        }

        [Test]
        public async Task Action_AbonementStillInFreePeriod_IsNotCounted()
        {
            int teacherId;
            using (var db = _services.CreateContext())
            {
                var seed = new Seed(db);
                var teacher = seed.Teacher(seed.User("Teacher"));
                var course = seed.Course(teacher);
                // 10 абонементов, у которых из 2 бесплатных занятий прошло только одно
                for (var i = 0; i < 10; i++)
                {
                    var abonement = seed.Abonement(course, seed.Student(), freeLessons: 2);
                    seed.Lesson(abonement, DateTime.UtcNow.AddDays(-7), LessonStatus.Happened, price: 0m);
                }
                teacherId = teacher.Id;
            }

            await Run();

            Assert.That(IndexOf(teacherId), Is.Null);
        }

        [Test]
        public async Task Action_UsesOnlyTenMostRecentAbonements()
        {
            var teacherId = SeedTeacher(abonements: 12, converted: 2);
            using (var db = _services.CreateContext())
            {
                // Конвертировавшиеся абонементы — самые старые, в выборку 10 последних они не попадают
                var seed = new Seed(db);
                var abonements = db.Abonements.Where(a => a.Course.TeacherId == teacherId).OrderBy(a => a.Id).ToArray();
                for (var i = 0; i < abonements.Length; i++)
                    seed.SetCreatedAt(abonements[i], DateTime.UtcNow.AddDays(-100 + i));
            }

            await Run();

            Assert.That(IndexOf(teacherId), Is.EqualTo(0f));
        }

        [Test]
        public async Task Action_BannedTeacher_IsNotRecalculated()
        {
            var teacherId = SeedTeacher(abonements: 10, converted: 10, isBanned: true, initialIndex: 0.1f);

            await Run();

            Assert.That(IndexOf(teacherId), Is.EqualTo(0.1f));
        }

        [Test]
        public async Task Action_TeacherWithFewAbonements_DoesNotStopRecalculationForOthers()
        {
            // Первым в выборке идёт преподаватель без нужного числа абонементов
            var newcomerId = SeedTeacher(abonements: 1, converted: 0);
            var experiencedId = SeedTeacher(abonements: 10, converted: 5);

            await Run();

            Assert.That(IndexOf(newcomerId), Is.Null);
            Assert.That(IndexOf(experiencedId), Is.EqualTo(0.5f).Within(0.0001f),
                "индекс остальных преподавателей тоже должен пересчитываться");
        }
    }
}
