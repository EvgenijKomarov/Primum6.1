using Common.Utilities;
using CoreDBIterator.Workers;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BackgroundWorkers
{
    public class LessonCreatingWorkerTests
    {
        private Mock<IServiceScopeFactory> _mockScopeFactory = null!;
        private Mock<IServiceProvider> _mockServiceProvider = null!;
        private Mock<PrimumContext> _mockContext = null!;
        private Mock<ConverterToDateTimeService> _mockDateTimeService = null!;
        private Mock<ILogger<LessonCreatingExecutor>> _mockLogger = null!;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<LessonCreatingExecutor>>();
            _mockContext = new Mock<PrimumContext>();

            // === Мок IConfiguration ===
            var сonfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "Constants:BlockedDaysForLessonCreation", "3" }
                    })
                .Build();

            // Создаём экземпляр сервиса с моком конфигурации
            _mockDateTimeService = new Mock<ConverterToDateTimeService>(сonfiguration);

            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockServiceProvider
                .Setup(x => x.GetService(typeof(PrimumContext)))
                .Returns(_mockContext.Object);
            _mockServiceProvider
                .Setup(x => x.GetService(typeof(ConverterToDateTimeService)))
                .Returns(_mockDateTimeService.Object); // ← передаём реальный экземпляр с моком конфигурации

            var mockScope = new Mock<IServiceScope>();
            mockScope.Setup(x => x.ServiceProvider).Returns(_mockServiceProvider.Object);

            _mockScopeFactory = new Mock<IServiceScopeFactory>();
            _mockScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(mockScope.Object);
        }

        [Test]
        public async Task Execute_WhenNoAbonementShedulesAvailable_DoesNotCreateLessons()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new List<AbonementShedule>());

            var executor = new LessonCreatingExecutor(_mockScopeFactory.Object, _mockLogger.Object);

            // Act
            await executor.Action();

            // Assert
            _mockContext.Verify(x => x.Set<Lesson>(), Times.Never);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Execute_WithEligibleAbonementShedules_CreatesLessonsAndUpdatesLastIteration()
        {
            // Arrange
            var course = new Course { CourseId = 10 };
            var abonement = new Abonement
            {
                AbonementId = 20,
                Course = course,
                PricePerLesson = 500,
                FreeLessons = 2,
                Lessons = new List<Lesson>() // пока нет уроков
            };
            var teacherShedule = new TeacherShedule
            {
                DayOfWeek = DayOfWeek.Wednesday,
                Time = 18
            };
            var shedule = new AbonementShedule
            {
                AbonementSheduleId = 30,
                Abonement = abonement,
                TeacherShedule = teacherShedule,
                LastIteration = DateTime.Now.AddDays(-8) // старше 7 дней → подходит
            };

            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new List<AbonementShedule> { shedule });
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson>());

            var nextDate = new DateTime(2026, 2, 5, 18, 0, 0); // среда
            _mockDateTimeService
                .Setup(x => x.GetNextSuitableDateThisWeek(DayOfWeek.Wednesday, It.IsAny<int>()))
                .Returns(nextDate);

            var executor = new LessonCreatingExecutor(_mockScopeFactory.Object, _mockLogger.Object);

            // Act
            await executor.Action();

            // Assert
            Assert.That(shedule.LastIteration, Is.EqualTo(nextDate));

            // Проверяем, что урок добавлен
            _mockContext.Verify(x => x.Set<Lesson>().Add(It.Is<Lesson>(l =>
                l.AbonementId == abonement.AbonementId &&
                l.DateTime == nextDate &&
                l.Price == 0 && // потому что FreeLessons (2) >= текущих уроков (0)
                l.Status == LessonStatus.Waiting
            )), Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);

            // Проверка логов
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Set LastIterationTime of {shedule.AbonementSheduleId}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Created lesson")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public async Task Execute_WhenFreeLessonsExhausted_SetsNonZeroPrice()
        {
            // Arrange
            var course = new Course { CourseId = 10, FreeLessons = 1 };
            var existingLesson = new Lesson { LessonId = 1, AbonementId = 20, DateTime = DateTime.Now, Price = 0 };
            var abonement = new Abonement
            {
                AbonementId = 20,
                Course = course,
                PricePerLesson = 600,
                Lessons = new List<Lesson> { existingLesson } // уже есть 1 урок → бесплатные исчерпаны
            };
            var teacherShedule = new TeacherShedule
            {
                DayOfWeek = DayOfWeek.Friday,
                Time = 19
            };
            var shedule = new AbonementShedule
            {
                AbonementSheduleId = 31,
                Abonement = abonement,
                TeacherShedule = teacherShedule,
                LastIteration = DateTime.Now.AddDays(-10)
            };

            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new List<AbonementShedule> { shedule });
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson> { existingLesson });

            var nextDate = new DateTime(2026, 2, 7, 19, 0, 0);
            _mockDateTimeService
                .Setup(x => x.GetNextSuitableDateThisWeek(DayOfWeek.Friday, It.IsAny<int>()))
                .Returns(nextDate);

            var executor = new LessonCreatingExecutor(_mockScopeFactory.Object, _mockLogger.Object);

            // Act
            await executor.Action();

            // Assert
            _mockContext.Verify(x => x.Set<Lesson>().Add(It.Is<Lesson>(l =>
                l.Price == 600 // не бесплатно!
            )), Times.Once);
        }

        [Test]
        public async Task Execute_RespectsFilterCondition_LastIterationOlderThan7Days()
        {
            // Arrange
            var recentShedule = new AbonementShedule
            {
                AbonementSheduleId = 40,
                Abonement = new Abonement { AbonementId = 1, Course = new Course { FreeLessons = 1 } },
                TeacherShedule = new TeacherShedule { DayOfWeek = DayOfWeek.Monday, Time = 10 },
                LastIteration = DateTime.Now.AddDays(-3) // только 3 дня назад → НЕ подходит
            };

            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new List<AbonementShedule> { recentShedule });

            var executor = new LessonCreatingExecutor(_mockScopeFactory.Object, _mockLogger.Object);

            // Act
            await executor.Action();

            // Assert: ничего не должно быть создано
            _mockContext.Verify(x => x.Set<Lesson>().Add(It.IsAny<Lesson>()), Times.Never);
            Assert.That(recentShedule.LastIteration, Is.EqualTo(DateTime.Now.AddDays(-3)).Within(TimeSpan.FromSeconds(1)));
        }
    }
}
