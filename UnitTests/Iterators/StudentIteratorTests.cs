using CoreConnection.Notifications;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Iterators;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class StudentIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private Mock<ConverterToDateTimeService> _mockDateTimeService = null!;
        private Mock<IPublisher> _mockPublisher = null!;
        private StudentIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _mockDateTimeService = new Mock<ConverterToDateTimeService>(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "Constants:BlockedDaysForLessonCreation", "3" }
                    })
                .Build());
            _mockPublisher = new Mock<IPublisher>();
            _iterator = new StudentIterator(_mockContext.Object, _mockDateTimeService.Object, _mockPublisher.Object);
        }

        #region GetStudentProfile

        [Test]
        public async Task GetStudentProfile_WhenStudentExists_ReturnsDto()
        {
            // Arrange
            var user = new User
            {
                Id = 101,
                StudentProfile = new StudentProfile()
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var result = await _iterator.GetStudentProfile(101);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.UserId, Is.EqualTo(101));
            });
        }

        [Test]
        public void GetStudentProfile_WhenStudentNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetStudentProfile(999));
        }

        #endregion

        #region SubscribeToCourse — успешный сценарий

        [Test]
        public async Task SubscribeToCourse_CreatesNewAbonementAndShedule_WhenNoExistingAbonement()
        {
            // Arrange
            var studentUser = new User
            {
                Id = 201,
                StudentProfile = new StudentProfile { Abonements = new List<Abonement>() }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            var course = new Course
            {
                CourseId = 301,
                Name = "Математика",
                Price = 500,
                MaxLessons = 10,
                FreeLessons = 2,
                ApproveStatus = ApproveStatus.Approved
            };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });
            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(new List<Abonement>());
            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(new List<AbonementShedule>());
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson>());

            var teacherUser = new User { Id = 401 };
            var teacherShedule = new TeacherShedule
            {
                TeacherSheduleId = 501,
                DayOfWeek = DayOfWeek.Monday,
                Time = 10,
                AbonementShedule = null,
                Teacher = new TeacherProfile
                {
                    User = teacherUser,
                    ApproveStatus = ApproveStatus.Approved
                }
            };
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { teacherShedule });

            var nextDate = new DateTime(2026, 2, 3, 10, 0, 0);
            _mockDateTimeService
                .Setup(x => x.GetNextFreeSuitableDateThisWeek(DayOfWeek.Monday, It.IsAny<int>()))
                .Returns(nextDate);

            // Act
            var result = await _iterator.SubscribeToCourse(201, 301, 501);

            // Assert

            // Проверяем создание абонемента
            Assert.That(studentUser.StudentProfile.Abonements.Count, Is.EqualTo(1));
            var abonement = studentUser.StudentProfile.Abonements.First();
            Assert.Multiple(() =>
            {
                Assert.That(abonement.Course.CourseId, Is.EqualTo(301));
                Assert.That(abonement.PricePerLesson, Is.EqualTo(500));
                Assert.That(abonement.AbonementStatus, Is.EqualTo(AbonementStatus.Active));
            });

            // Проверяем расписание
            Assert.That(abonement.AbonementShedules.Count, Is.EqualTo(1));
            var shedule = abonement.AbonementShedules.First();
            Assert.That(shedule.LastIteration, Is.EqualTo(nextDate));

            // Проверяем урок
            _mockContext.Verify(x => x.Set<Lesson>().AddAsync(It.Is<Lesson>(l =>
                l.Price == 0 && // потому что FreeLessons (2) >= Lessons.Count() (0)
                l.DateTime == nextDate &&
                l.Status == LessonStatus.Waiting
            ), It.IsAny<System.Threading.CancellationToken>()), Times.Once);

            // Проверяем публикацию
            _mockPublisher.Verify(
                x => x.PublishAsync(It.Is<NewAbonementSheduleNotification>(n =>
                    n.StudentUserId == 201 &&
                    n.TeacherUserId == 401 &&
                    n.CourseName == "Математика" &&
                    n.DayOfWeek == "Monday")), Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        #endregion

        #region Проверки исключений

        [Test]
        public void SubscribeToCourse_WhenStudentNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { new Course { CourseId = 1, ApproveStatus = ApproveStatus.Approved } });
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { new TeacherShedule { TeacherSheduleId = 1, AbonementShedule = null, Teacher = new TeacherProfile { ApproveStatus = ApproveStatus.Approved } } });

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.SubscribeToCourse(999, 1, 1));
        }

        [Test]
        public void SubscribeToCourse_WhenCourseNotFoundOrNotAvailable_ThrowsException()
        {
            var student = new User { Id = 202, StudentProfile = new StudentProfile() };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { student });

            var unavailableCourse = new Course { CourseId = 2, ApproveStatus = ApproveStatus.NeedModeratorReview, IsActive = false };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { unavailableCourse });

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.SubscribeToCourse(202, 2, 1));
        }

        [Test]
        public void SubscribeToCourse_WhenTeacherNotApproved_ThrowsException()
        {
            var student = new User { Id = 203, StudentProfile = new StudentProfile() };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { student });

            var course = new Course { CourseId = 3, ApproveStatus = ApproveStatus.Approved };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });

            var unapprovedShedule = new TeacherShedule
            {
                TeacherSheduleId = 2,
                AbonementShedule = null,
                Teacher = new TeacherProfile { ApproveStatus = ApproveStatus.NeedModeratorReview } // не одобрен
            };
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { unapprovedShedule });

            Assert.ThrowsAsync<NotAvailableException>(async () => await _iterator.SubscribeToCourse(203, 3, 2));
        }

        [Test]
        public void SubscribeToCourse_WhenSheduleNotFoundOrBusy_ThrowsException()
        {
            var student = new User { Id = 204, StudentProfile = new StudentProfile() };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { student });

            var course = new Course { CourseId = 4, ApproveStatus = ApproveStatus.Approved };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });

            // Занятое расписание
            var busyShedule = new TeacherShedule
            {
                TeacherSheduleId = 3,
                AbonementShedule = new AbonementShedule(),
                Teacher = new TeacherProfile { ApproveStatus = ApproveStatus.Approved }
            };
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { busyShedule });

            Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.SubscribeToCourse(204, 4, 3));
        }

        [Test]
        public void SubscribeToCourse_WhenStudentSubscribesToHimself_ThrowsException()
        {
            var student = new User { Id = 205, StudentProfile = new StudentProfile() };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { student });

            var course = new Course { CourseId = 5, ApproveStatus = ApproveStatus.Approved };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });

            var selfShedule = new TeacherShedule
            {
                TeacherSheduleId = 4,
                AbonementShedule = null,
                Teacher = new TeacherProfile
                {
                    User = new User { Id = 205 }, // тот же ID
                    ApproveStatus = ApproveStatus.Approved
                }
            };
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { selfShedule });

            Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.SubscribeToCourse(205, 5, 4));
        }

        [Test]
        public void SubscribeToCourse_WhenSameSheduleAlreadyExists_ThrowsException()
        {
            var teacherShedule = new TeacherShedule
            {
                DayOfWeek = DayOfWeek.Tuesday,
                Time = 14
            };
            var existingShedule = new AbonementShedule
            {
                TeacherShedule = teacherShedule
            };
            existingShedule.TeacherShedule = teacherShedule;
            var abonement = new Abonement
            {
                CourseId = 6,
                AbonementShedules = new List<AbonementShedule> { existingShedule }
            };
            var student = new User
            {
                Id = 206,
                StudentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { student });

            var course = new Course { CourseId = 6, ApproveStatus = ApproveStatus.Approved };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });

            var duplicateShedule = new TeacherShedule
            {
                TeacherSheduleId = 5,
                DayOfWeek = DayOfWeek.Tuesday,
                Time = 14,
                AbonementShedule = null,
                Teacher = new TeacherProfile
                {
                    User = new User { Id = 105 }
                }
            };
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { duplicateShedule });

            Assert.ThrowsAsync<NotAvailableException>(async () => await _iterator.SubscribeToCourse(206, 6, 5));
        }

        [Test]
        public void SubscribeToCourse_WhenExceedsMaxLessons_ThrowsException()
        {
            // Arrange
            var abonement = new Abonement
            {
                CourseId = 7,
                AbonementShedules = new List<AbonementShedule>
                {
                    new AbonementShedule
                    {
                        TeacherShedule = new TeacherShedule
                        {
                            DayOfWeek = DayOfWeek.Monday,
                            Time = 1
                        }
                    },
                    new AbonementShedule
                    {
                        TeacherShedule = new TeacherShedule
                        {
                            DayOfWeek = DayOfWeek.Wednesday,
                            Time = 1
                        }
                    },
                    new AbonementShedule
                    {
                        TeacherShedule = new TeacherShedule
                        {
                            DayOfWeek = DayOfWeek.Friday,
                            Time = 1
                        }
                    }
                }
            };
            var student = new User
            {
                Id = 207,
                StudentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { student });

            var course = new Course { CourseId = 7, ApproveStatus = ApproveStatus.Approved, MaxLessons = 3 }; // максимум 3
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });

            var newShedule = new TeacherShedule
            {
                TeacherSheduleId = 6,
                DayOfWeek = DayOfWeek.Wednesday,
                Time = 16,
                AbonementShedule = null,
                Teacher = new TeacherProfile { ApproveStatus = ApproveStatus.Approved, User = new User() { Id = 107 } }
            };
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(new[] { newShedule });

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.SubscribeToCourse(207, 7, 6));
            Assert.That(ex?.Message, Does.Contain("maximum shedules per week"));
        }

        #endregion
    }
}
