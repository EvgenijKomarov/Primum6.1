using System.Globalization;
using System.Net;
using System.Web;
using CoreDBIterator.Workers;
using CoreDBIteratorTests.Infrastructure;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreDBIteratorTests.Unit
{
    public class LessonIteratorExecutorTests
    {
        private const decimal Price = 1000m;

        private TestServices _services = null!;
        private LessonScenario _scenario = null!;

        [SetUp]
        public void SetUp()
        {
            _services = new TestServices();
            using var db = _services.CreateContext();
            _scenario = LessonScenario.Create(db, Price);
            _services.External.ReadyTeachers[_scenario.TeacherUserId] = true;
            _services.External.Balances[_scenario.StudentUserId] = 5000m;
        }

        [TearDown]
        public void TearDown() => _services.Dispose();

        private int AddLesson(
            DateTime? at = null,
            LessonStatus status = LessonStatus.Warned,
            decimal price = Price,
            bool isWorkoff = false)
        {
            using var db = _services.CreateContext();
            var abonement = db.Abonements.Single(a => a.Id == _scenario.AbonementId);
            return new Seed(db).Lesson(abonement, at ?? DateTime.UtcNow.AddMinutes(20), status, price, isWorkoff).Id;
        }

        private Lesson LoadLesson(int id)
        {
            using var db = _services.CreateContext();
            return db.Lessons.Include(l => l.Abonement).AsNoTracking().Single(l => l.Id == id);
        }

        private Task Run() => _services.CreateWorker<LessonIteratorExecutor>().Action();

        [Test]
        public async Task Action_PaidLesson_HappensWithLinksAndTeacherEarning()
        {
            var lessonId = AddLesson();

            await Run();

            var lesson = LoadLesson(lessonId);
            Assert.Multiple(() =>
            {
                Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Happened));
                Assert.That(lesson.StudentLink, Does.StartWith("https://meet.jit.si/").And.EndWith("userType=guest"));
                Assert.That(lesson.TeacherLink, Does.StartWith("https://meet.jit.si/").And.EndWith("userType=admin"));
                Assert.That(lesson.TeacherEarning, Is.GreaterThan(0m).And.LessThan(Price));
            });
        }

        [Test]
        public async Task Action_PaidLesson_SplitsFullPriceBetweenTeacherAndPlatform()
        {
            var lessonId = AddLesson();

            await Run();

            var payment = _services.External.PaymentRequests("/process-lesson-payment").Single();
            var query = HttpUtility.ParseQueryString(payment.Query);
            decimal Amount(string key) => decimal.Parse(query[key]!, CultureInfo.InvariantCulture);
            Assert.Multiple(() =>
            {
                Assert.That(int.Parse(query["lessonId"]!), Is.EqualTo(lessonId));
                Assert.That(int.Parse(query["studentUserId"]!), Is.EqualTo(_scenario.StudentUserId));
                Assert.That(int.Parse(query["teacherUserId"]!), Is.EqualTo(_scenario.TeacherUserId));
                Assert.That(Amount("teacherCash") + Amount("platformCash"), Is.EqualTo(Price));
                Assert.That(Amount("teacherCash"), Is.EqualTo(LoadLesson(lessonId).TeacherEarning));
            });
        }

        [Test]
        public async Task Action_NotEnoughMoney_MarksLessonMissedWithoutCharging()
        {
            _services.External.Balances[_scenario.StudentUserId] = Price - 1;
            var lessonId = AddLesson();

            await Run();

            Assert.That(LoadLesson(lessonId).Status, Is.EqualTo(LessonStatus.Missed));
            Assert.That(_services.External.PaymentRequests("/process-lesson-payment"), Is.Empty);
        }

        [Test]
        public async Task Action_PaymentRejected_LessonDoesNotHappen()
        {
            _services.External.PaymentResult = false;
            var lessonId = AddLesson();

            await Run();

            var lesson = LoadLesson(lessonId);
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.MissedDueToException),
                "платёжный сервис отказал в списании — урок не может считаться проведённым");
            Assert.That(lesson.StudentLink, Is.Null);
        }

        [Test]
        public async Task Action_FreeLessonWithZeroBalance_Happens()
        {
            _services.External.Balances[_scenario.StudentUserId] = 0m;
            var lessonId = AddLesson(price: 0m);

            await Run();

            Assert.That(LoadLesson(lessonId).Status, Is.EqualTo(LessonStatus.Happened));
        }

        [Test]
        public async Task Action_TeacherNotReadyForPayments_MarksMissedDueToException()
        {
            _services.External.ReadyTeachers[_scenario.TeacherUserId] = false;
            var lessonId = AddLesson();

            await Run();

            Assert.That(LoadLesson(lessonId).Status, Is.EqualTo(LessonStatus.MissedDueToException));
            Assert.That(_services.External.PaymentRequests("/process-lesson-payment"), Is.Empty);
        }

        [Test]
        public async Task Action_MissedWorkoffLesson_ReturnsCancelledLessonCredit()
        {
            _services.External.Balances[_scenario.StudentUserId] = 0m;
            var lessonId = AddLesson(isWorkoff: true);

            await Run();

            Assert.That(LoadLesson(lessonId).Abonement.CancelledLessons, Is.EqualTo(1));
        }

        [Test]
        public async Task Action_IgnoresLessonsLaterThan30MinutesAndNotWarned()
        {
            var later = AddLesson(DateTime.UtcNow.AddHours(2));
            var waiting = AddLesson(DateTime.UtcNow.AddMinutes(10), LessonStatus.Waiting);

            await Run();

            Assert.Multiple(() =>
            {
                Assert.That(LoadLesson(later).Status, Is.EqualTo(LessonStatus.Warned));
                Assert.That(LoadLesson(waiting).Status, Is.EqualTo(LessonStatus.Waiting));
            });
        }

        [Test]
        public async Task Action_NotifiesStudentAndTeacherByTheirUserIds()
        {
            AddLesson();

            await Run();

            var notified = _services.External.NotifiedUserIds;
            Assert.That(notified, Does.Contain(_scenario.StudentUserId));
            Assert.That(notified, Does.Contain(_scenario.TeacherUserId), "преподаватель должен получить ссылку на занятие");
            Assert.That(notified, Does.Not.Contain(_scenario.OutsiderUserId), "ссылка ведущего ушла постороннему пользователю");
        }

        [Test]
        public async Task Action_NotificationFailureAfterPayment_KeepsLessonHappened()
        {
            _services.External.NotificationStatus = HttpStatusCode.InternalServerError;
            var lessonId = AddLesson();

            await Run();

            // Оплата уже прошла: из-за упавшего уведомления урок не должен считаться сорванным
            Assert.That(_services.External.PaymentRequests("/process-lesson-payment"), Has.Exactly(1).Items);
            var lesson = LoadLesson(lessonId);
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Happened));
            Assert.That(lesson.StudentLink, Is.Not.Null);
        }

        [Test]
        public async Task Action_OneFailingLessonDoesNotBlockOthers()
        {
            int otherLessonId;
            using (var db = _services.CreateContext())
            {
                var seed = new Seed(db);
                var otherTeacher = seed.Teacher(seed.User("NotReadyTeacher"));
                var abonement = seed.Abonement(seed.Course(otherTeacher, Price), db.StudentProfiles.First());
                otherLessonId = seed.Lesson(abonement, DateTime.UtcNow.AddMinutes(20), LessonStatus.Warned).Id;
            }
            var lessonId = AddLesson();

            await Run();

            Assert.That(LoadLesson(otherLessonId).Status, Is.EqualTo(LessonStatus.MissedDueToException));
            Assert.That(LoadLesson(lessonId).Status, Is.EqualTo(LessonStatus.Happened));
        }
    }
}
