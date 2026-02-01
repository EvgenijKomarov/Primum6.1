using CoreConnection.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.BackgroundWorkers.Executors;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BackgroundWorkers
{
    public class LessonIteratorWorkerTests
    {
        private Mock<IServiceScopeFactory> _mockScopeFactory = null!;
        private Mock<IServiceProvider> _mockServiceProvider = null!;
        private Mock<IPrimumContext> _mockContext = null!;
        private Mock<IPublisher> _mockPublisher = null!;
        private Mock<ILogger> _mockLogger = null!;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger>();
            _mockContext = new Mock<IPrimumContext>();
            _mockPublisher = new Mock<IPublisher>();

            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockServiceProvider
                .Setup(x => x.GetService(typeof(IPrimumContext)))
                .Returns(_mockContext.Object);
            _mockServiceProvider
                .Setup(x => x.GetService(typeof(IPublisher)))
                .Returns(_mockPublisher.Object);

            var mockScope = new Mock<IServiceScope>();
            mockScope.Setup(x => x.ServiceProvider).Returns(_mockServiceProvider.Object);

            _mockScopeFactory = new Mock<IServiceScopeFactory>();
            _mockScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(mockScope.Object);
        }

        [Test]
        public async Task Execute_WhenNoLessonsInWarnedStatus_DoesNothing()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson>());

            var executor = new LessonIteratorExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Execute_LessonHappened_WhenStudentHasEnoughCashAndAbonementIsActive()
        {
            // Arrange
            var studentUser = new User { Id = 101, Cash = 1000 };
            var teacherUser = new User { Id = 201, Cash = 5000 };
            var course = new Course
            {
                CourseId = 301,
                Name = "Математика",
                Teacher = new TeacherProfile
                {
                    TeacherId = 201,
                    User = teacherUser,
                    EarningMultiplier = 0.8f
                }
            };
            var abonement = new Abonement
            {
                AbonementId = 401,
                Student = new StudentProfile { User = studentUser },
                Course = course,
                PricePerLesson = 500,
                AbonementStatus = AbonementStatus.Active
            };
            var lesson = new Lesson
            {
                LessonId = 501,
                Abonement = abonement,
                Price = 500,
                Status = LessonStatus.Warned,
                DateTime = DateTime.Now.AddMinutes(10) // попадает в окно
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });
            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new List<AbonementShedule>());

            var executor = new LessonIteratorExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Happened));
            Assert.That(studentUser.Cash, Is.EqualTo(500)); // 1000 - 500
            Assert.That(teacherUser.Cash, Is.EqualTo(5000 + 400)); // 500 * 0.8 = 400

            Assert.That(lesson.StudentLink, Does.StartWith("https://meet.jit.si/"));
            Assert.That(lesson.TeacherLink, Does.StartWith("https://meet.jit.si/"));
            Assert.That(lesson.StudentLink, Does.Contain("userType=guest"));
            Assert.That(lesson.TeacherLink, Does.Contain("userType=admin"));

            _mockPublisher.Verify(
                x => x.PublishAsync(It.Is<LessonNotification>(n =>
                    n.LessonId == lesson.LessonId &&
                    n.StudentUserId == studentUser.Id &&
                    n.TeacherUserId == course.TeacherId &&
                    n.StudentLink == lesson.StudentLink &&
                    n.TeacherLink == lesson.TeacherLink)),
                Times.Once);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Lesson {lesson.LessonId} happened successfully")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public async Task Execute_LessonMissedAndAbonementDeleted_WhenNotEnoughCashButAbonementActive()
        {
            // Arrange
            var studentUser = new User { Id = 102, Cash = 100 };
            var teacherUser = new User { Id = 202, Cash = 3000 };
            var course = new Course
            {
                CourseId = 302,
                Name = "Физика",
                Teacher = new TeacherProfile { TeacherId = 202, User = teacherUser, EarningMultiplier = 1.0f }
            };
            var abonementShedule = new AbonementShedule { AbonementSheduleId = 601 };
            var abonement = new Abonement
            {
                AbonementId = 402,
                Student = new StudentProfile { User = studentUser },
                Course = course,
                PricePerLesson = 500,
                AbonementStatus = AbonementStatus.Active,
                AbonementShedules = new List<AbonementShedule> { abonementShedule }
            };
            var lesson = new Lesson
            {
                LessonId = 502,
                Abonement = abonement,
                Price = 500,
                Status = LessonStatus.Warned,
                DateTime = DateTime.Now.AddMinutes(20)
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });
            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new[] { abonementShedule });

            var executor = new LessonIteratorExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Missed));
            Assert.That(abonement.AbonementStatus, Is.EqualTo(AbonementStatus.Deleted));
            Assert.That(studentUser.Cash, Is.EqualTo(100)); // не списано
            Assert.That(teacherUser.Cash, Is.EqualTo(3000)); // не начислено

            _mockContext.Verify(x => x.Set<AbonementShedule>().RemoveRange(It.Is<IEnumerable<AbonementShedule>>(list =>
                list.Contains(abonementShedule))), Times.Once);

            _mockPublisher.Verify(
                x => x.PublishAsync(It.Is<LessonFailureNotification>(n =>
                    n.LessonId == lesson.LessonId &&
                    n.StudentUserId == studentUser.Id)),
                Times.Once);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Lesson {lesson.LessonId} not happened and deleted abonement")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [TestCase(AbonementStatus.Freezed)]
        [TestCase(AbonementStatus.Deleted)]
        public async Task Execute_LessonMissed_WhenAbonementNotActive(AbonementStatus status)
        {
            // Arrange
            var studentUser = new User { Id = 103, Cash = 1000 };
            var teacherUser = new User { Id = 203, Cash = 2000 };
            var course = new Course
            {
                CourseId = 303,
                Name = "Химия",
                Teacher = new TeacherProfile { TeacherId = 203, User = teacherUser }
            };
            var abonement = new Abonement
            {
                AbonementId = 403,
                Student = new StudentProfile { User = studentUser },
                Course = course,
                PricePerLesson = 500,
                AbonementStatus = status // ← не активен!
            };
            var lesson = new Lesson
            {
                LessonId = 503,
                Abonement = abonement,
                Price = 500,
                Status = LessonStatus.Warned,
                DateTime = DateTime.Now.AddMinutes(15)
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            var executor = new LessonIteratorExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Missed));
            Assert.That(abonement.AbonementStatus, Is.EqualTo(status)); // не изменён
            Assert.That(studentUser.Cash, Is.EqualTo(1000));
            Assert.That(teacherUser.Cash, Is.EqualTo(2000));

            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
            _mockContext.Verify(x => x.Set<AbonementShedule>().RemoveRange(It.IsAny<IEnumerable<AbonementShedule>>()), Times.Never);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Lesson {lesson.LessonId} missed")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public async Task Execute_IgnoresLessonsOutsideTimeWindow()
        {
            // Arrange
            var lesson = new Lesson
            {
                LessonId = 504,
                Status = LessonStatus.Warned,
                DateTime = DateTime.Now.AddHours(1) // слишком далеко в будущем
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            var executor = new LessonIteratorExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert: ничего не должно произойти
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Warned));
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }
    }
}
