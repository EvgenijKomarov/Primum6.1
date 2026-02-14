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
    public class LessonWarningWorkerTests
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
        public async Task Execute_WhenNoLessonsInWaitingStatus_DoesNothing()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson>());

            var executor = new LessonWarningExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Execute_WarnsLessonsWithin24HoursAndPublishesNotification()
        {
            // Arrange
            var studentUser = new User { Id = 101, Cash = 800 };
            var teacherUser = new User { Id = 201 };
            var course = new Course
            {
                CourseId = 301,
                Name = "Английский",
                Teacher = new TeacherProfile { TeacherId = 201, User = teacherUser }
            };
            var abonement = new Abonement
            {
                AbonementId = 401,
                Student = new StudentProfile { User = studentUser },
                Course = course,
                PricePerLesson = 500
            };
            var lesson = new Lesson
            {
                LessonId = 501,
                Abonement = abonement,
                Price = 500,
                Status = LessonStatus.Waiting,
                DateTime = DateTime.Now.AddHours(12) // < 24h → подходит
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            var executor = new LessonWarningExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Warned));

            _mockPublisher.Verify(
                x => x.PublishAsync(It.Is<LessonPreparationNotification>(n =>
                    n.LessonId == lesson.LessonId &&
                    n.StudentUserId == studentUser.Id &&
                    n.TeacherUserId == course.TeacherId &&
                    n.CourseName == course.Name &&
                    n.IsEnoughMoney == true // 800 >= 500
                )),
                Times.Once);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Lesson {lesson.LessonId} warned")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public async Task Execute_HandlesMultipleLessonsCorrectly()
        {
            // Arrange
            var lessons = new List<Lesson>();
            for (int i = 1; i <= 3; i++)
            {
                var user = new User { Id = 100 + i, Cash = 1000 };
                var course = new Course
                {
                    CourseId = 300 + i,
                    Name = $"Курс{i}",
                    Teacher = new TeacherProfile { TeacherId = 200 + i, User = new User { Id = 200 + i } }
                };
                var abonement = new Abonement
                {
                    AbonementId = 400 + i,
                    Student = new StudentProfile { User = user },
                    Course = course,
                    PricePerLesson = 300
                };
                lessons.Add(new Lesson
                {
                    LessonId = 500 + i,
                    Abonement = abonement,
                    Price = 300,
                    Status = LessonStatus.Waiting,
                    DateTime = DateTime.Now.AddHours(23)
                });
            }

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(lessons);

            var executor = new LessonWarningExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            foreach (var lesson in lessons)
            {
                Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Warned));
            }

            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<LessonPreparationNotification>()), Times.Exactly(3));
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("warned")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Exactly(3));
        }

        [Test]
        public async Task Execute_IgnoresLessonsOutside24HourWindow()
        {
            // Arrange
            var lesson = new Lesson
            {
                LessonId = 502,
                Status = LessonStatus.Waiting,
                DateTime = DateTime.Now.AddDays(2) // > 24h → не подходит
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            var executor = new LessonWarningExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            Assert.That(lesson.Status, Is.EqualTo(LessonStatus.Waiting)); // не изменён
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }

        [Test]
        public async Task Execute_SetsIsEnoughMoneyCorrectlyWhenInsufficientFunds()
        {
            // Arrange
            var studentUser = new User { Id = 102, Cash = 200 };
            var teacherUser = new User { Id = 202 };
            var course = new Course
            {
                CourseId = 302,
                Name = "Программирование",
                Teacher = new TeacherProfile { TeacherId = 202, User = teacherUser }
            };
            var abonement = new Abonement
            {
                AbonementId = 402,
                Student = new StudentProfile { User = studentUser },
                Course = course,
                PricePerLesson = 500
            };
            var lesson = new Lesson
            {
                LessonId = 502,
                Abonement = abonement,
                Price = 500,
                Status = LessonStatus.Waiting,
                DateTime = DateTime.Now.AddHours(1)
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            var executor = new LessonWarningExecutor(_mockScopeFactory.Object);

            // Act
            await executor.Execute(_mockLogger.Object);

            // Assert
            _mockPublisher.Verify(
                x => x.PublishAsync(It.Is<LessonPreparationNotification>(n =>
                    n.IsEnoughMoney == false // 200 < 500
                )),
                Times.Once);
        }
    }
}
