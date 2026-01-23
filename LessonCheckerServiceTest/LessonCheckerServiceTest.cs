using DataNotifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServiceTests
{
    public class LessonCheckerServiceTest
    {
        /*
        private Mock<DbContextFactory<IPrimumContext>> _contextFactoryMock;
        private Mock<JitsiLinkCreationService> _jitsiServiceMock;
        private Mock<CoreNotificationService> _notificationServiceMock;
        private LessonCheckerService _service;
        private Mock<IPrimumContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _contextFactoryMock = new Mock<DbContextFactory<IPrimumContext>>("testConnectionString");
            _jitsiServiceMock = new Mock<JitsiLinkCreationService>(new Mock<ILogger<JitsiLinkCreationService>>().Object);
            _notificationServiceMock = new Mock<CoreNotificationService>(new Mock<IPublisherFactory>().Object, new Mock<ILogger<CoreNotificationService>>().Object);
            _contextMock = new Mock<IPrimumContext>();

            _contextFactoryMock.Setup(x => x.CreateDbContext()).Returns(_contextMock.Object);

            _service = new LessonCheckerService(
                _contextFactoryMock.Object,
                _jitsiServiceMock.Object,
                _notificationServiceMock.Object);
        }

        [Test]
        public async Task IterateAsync_ShouldSendPreparationNotification_WhenLessonIsWithin24Hours()
        {
            // Arrange
            var testDateTime = DateTime.Now.AddHours(23); // Within 24 hours
            var lesson = new Lesson()
            {
                 Status = LessonStatus.Waiting,
                 DateTime = testDateTime,
                 Abonement = new Abonement
                 {
                     StudentId = 1,
                     Course = new Course { TeacherId = 2 },
                     PaidLessons = 1
                 }
            };
            var lessons = new List<Lesson>
            {
                lesson
            }
            .AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);

            // Act
            await _service.IterateAsync();

            // Assert
            _notificationServiceMock.Verify(x => x.PublishAsync(It.Is<LessonPreparationNotification>(n =>
                n.StudentUserId == 1 &&
                n.TeacherUserId == 2 &&
                n.IsEnoughPaidLessons == true)),
                Times.Once);
        }

        [Test]
        public async Task IterateAsync_ShouldUpdateLessonStatusToWarned_WhenSendingPreparationNotification()
        {
            // Arrange
            var testDateTime = DateTime.Now.AddHours(23);
            var lessons = new List<Lesson>
            {
                new Lesson
                {
                    Status = LessonStatus.Waiting,
                    DateTime = testDateTime,
                    Abonement = new Abonement
                    {
                        StudentId = 1,
                        Course = new Course { TeacherId = 2 },
                        PaidLessons = 1
                    }
                }
            }
            .AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);

            // Act
            await _service.IterateAsync();

            // Assert
            Assert.AreEqual(LessonStatus.Warned, lessons.First().Status);
        }

        [Test]
        public async Task IterateAsync_ShouldCreateJitsiMeetingAndSendNotification_WhenLessonIsWithin30MinutesAndPaid()
        {
            // Arrange
            var testDateTime = DateTime.Now.AddMinutes(20);
            var lessons = new List<Lesson>
            {
                new Lesson
                {
                    Status = LessonStatus.Warned,
                    DateTime = testDateTime,
                    Abonement = new Abonement
                    {
                        StudentId = 1,
                        Course = new Course { TeacherId = 2 },
                        PaidLessons = 1,
                        AbonementStatus = AbonementStatus.Active
                    }
                }
            }
            .AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);
            _jitsiServiceMock.Setup(x => x.CreateJitsiMeeting(It.IsAny<string>()))
                .Returns(("adminLink", "guestLink"));

            // Act
            await _service.IterateAsync();

            // Assert
            _jitsiServiceMock.Verify(x => x.CreateJitsiMeeting(It.IsAny<string>()), Times.Once);
            _notificationServiceMock.Verify(x => x.PublishAsync(It.Is<LessonNotification>(n =>
                n.StudentLink == "guestLink" &&
                n.TeacherLink == "adminLink" &&
                n.StudentUserId == 1 &&
                n.TeacherUserId == 2)),
                Times.Once);
        }

        [Test]
        public async Task IterateAsync_ShouldDecrementPaidLessons_WhenLessonHappens()
        {
            // Arrange
            var testDateTime = DateTime.Now.AddMinutes(20);
            var abonement = new Abonement
            {
                StudentId = 1,
                Course = new Course { TeacherId = 2 },
                PaidLessons = 5,
                AbonementStatus = AbonementStatus.Active
            };

            var lessons = new List<Lesson>
        {
            new Lesson
            {
                Status = LessonStatus.Warned,
                DateTime = testDateTime,
                Abonement = abonement
            }
        }.AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);

            // Act
            await _service.IterateAsync();

            // Assert
            Assert.AreEqual(4, abonement.PaidLessons);
            Assert.AreEqual(LessonStatus.Happened, lessons.First().Status);
        }

        [Test]
        public async Task IterateAsync_ShouldMarkAbonementAsDeleted_WhenLessonIsNotPaid()
        {
            // Arrange
            var testDateTime = DateTime.Now.AddMinutes(20);
            var abonement = new Abonement
            {
                StudentId = 1,
                Course = new Course { TeacherId = 2, Name = "Test Course" },
                PaidLessons = 0,
                AbonementStatus = AbonementStatus.Active,
                AbonementShedules = new List<AbonementShedule>
                {
                    new AbonementShedule(),
                    new AbonementShedule()
                }
            };

            var lessons = new List<Lesson>
            {
                new Lesson
                {
                    Status = LessonStatus.Warned,
                    DateTime = testDateTime,
                    Abonement = abonement
                }
            }.AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);
            var abonementShedulesDbSetMock = new Mock<DbSet<AbonementShedule>>();
            _contextMock.Setup(x => x.Set<AbonementShedule>()).Returns(abonementShedulesDbSetMock.Object);

            // Act
            await _service.IterateAsync();

            // Assert
            Assert.AreEqual(AbonementStatus.Deleted, abonement.AbonementStatus);
            abonementShedulesDbSetMock.Verify(x => x.RemoveRange(abonement.AbonementShedules), Times.Once);
            _notificationServiceMock.Verify(x => x.PublishAsync(It.Is<LessonFailureNotification>(n =>
                n.CourseName == "Test Course" &&
                n.StudentUserId == 1 &&
                n.TeacherUserId == 2 &&
                n.DateTime == lessons.First().DateTime)),
                Times.Once);
        }

        [TestCase(AbonementStatus.Deleted)]
        [TestCase(AbonementStatus.Freezed)]
        public async Task IterateAsync_ShouldMarkLessonAsMissed_WhenNotPaidAndNotActive(AbonementStatus status)
        {
            // Arrange
            var testDateTime = DateTime.Now.AddMinutes(20);
            var abonementShedules = new List<AbonementShedule>()
            {
                new AbonementShedule()
                {
                    AbonementSheduleId = 1
                }
            };
            var abonement = new Abonement
            {
                StudentId = 1,
                Course = new Course { TeacherId = 2 },
                PaidLessons = 1,
                AbonementStatus = status,
                AbonementShedules = abonementShedules
            };

            var lessons = new List<Lesson>
        {
            new Lesson
            {
                Status = LessonStatus.Warned,
                DateTime = testDateTime,
                Abonement = abonement
            }
        }.AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);
            _contextMock.Setup(x => x.Set<AbonementShedule>()).ReturnsDbSet(new List<AbonementShedule>()
            {

            });

            // Act
            await _service.IterateAsync();

            // Assert
            Assert.AreEqual(LessonStatus.Missed, lessons.First().Status);
            _contextMock.Verify(x => 
                x.Set<AbonementShedule>().RemoveRange(It.Is<ICollection<AbonementShedule>>(y => 
                    y.SequenceEqual(abonement.AbonementShedules))), Times.Never);
            _notificationServiceMock.Verify(x => x.PublishAsync(It.IsAny<LessonFailureNotification>()), Times.Never);
        }

        [Test]
        public async Task IterateAsync_ShouldNotProcessLessons_ThatDontMeetTimeCriteria()
        {
            // Arrange
            var lessons = new List<Lesson>
            {
                new Lesson // Too far in future for preparation notification
                {
                    Status = LessonStatus.Waiting,
                    DateTime = DateTime.Now.AddDays(2),
                    Abonement = new Abonement { PaidLessons = 1 }
                },
                new Lesson // Too far in future for lesson notification
                {
                    Status = LessonStatus.Warned,
                    DateTime = DateTime.Now.AddHours(1),
                    Abonement = new Abonement { PaidLessons = 1 }
                }
            }.AsQueryable();

            _contextMock.Setup(x => x.Set<Lesson>()).ReturnsDbSet(lessons);

            // Act
            await _service.IterateAsync();

            // Assert
            _notificationServiceMock.Verify(x => x.PublishAsync(It.IsAny<LessonPreparationNotification>()), Times.Never);
            _notificationServiceMock.Verify(x => x.PublishAsync(It.IsAny<LessonNotification>()), Times.Never);
        }*/
    }
}
