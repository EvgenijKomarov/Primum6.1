using CoreConnection.Enums;
using CoreConnection.Notifications;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Iterators;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class AbonementIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private Mock<IPublisher> _mockPublisher = null!;
        private AbonementIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _mockPublisher = new Mock<IPublisher>();
            _iterator = new AbonementIterator(_mockContext.Object, _mockPublisher.Object);
        }

        #region GetTeacherAbonements

        [Test]
        public async Task GetTeacherAbonements_WhenTeacherExists_ReturnsMappedAbonements()
        {
            // Arrange
            var studentUser = new User { Id = 101 };
            var courseTheme = new CourseTheme { CourseThemeId = 50, ThemeName = "Грамматика" };
            var abon = new Abonement()
            {
                AbonementId = 301,
                PricePerLesson = 600,
                AbonementStatus = AbonementStatus.Active,
                Student = new StudentProfile { User = studentUser }
            };
            var course = new Course
            {
                CourseId = 201,
                Name = "Английский",
                CourseTheme = courseTheme,
                Abonements = new List<Abonement>{ abon }
            };
            abon.Course = course;
            var teacherProfile = new TeacherProfile
            {
                TeacherId = 1,
                Courses = new List<Course> { course }
            };
            var teacherUser = new User
            {
                Id = 1,
                TeacherProfile = teacherProfile
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            // Act
            var result = await _iterator.GetTeacherAbonements(1);

            // Assert
            var abonementDto = result.Single();
            Assert.That(abonementDto.StudentId, Is.EqualTo(101));
            Assert.That(abonementDto.TeacherId, Is.EqualTo(1));
            Assert.That(abonementDto.CourseName, Is.EqualTo("Английский"));
            Assert.That(abonementDto.CourseThemeName, Is.EqualTo("Грамматика"));
            Assert.That(abonementDto.PricePerLesson, Is.EqualTo(600));
            Assert.That(abonementDto.AbonementStatus, Is.EqualTo(StatusAbonement.Active));
        }

        [Test]
        public void GetTeacherAbonements_WhenTeacherNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetTeacherAbonements(999));
        }

        #endregion

        #region GetStudentAbonements

        [Test]
        public async Task GetStudentAbonements_WhenStudentExists_ReturnsMappedAbonements()
        {
            // Arrange
            var teacherUser = new User { Id = 201 };
            var courseTheme = new CourseTheme { CourseThemeId = 51, ThemeName = "Алгоритмы" };
            var course = new Course
            {
                CourseId = 202,
                Name = "C#",
                CourseTheme = courseTheme,
                Teacher = new TeacherProfile { TeacherId = 201, User = teacherUser }
            };
            var abonement = new Abonement
            {
                AbonementId = 302,
                PricePerLesson = 800,
                AbonementStatus = AbonementStatus.Freezed,
                Course = course
            };
            var studentProfile = new StudentProfile
            {
                Abonements = new List<Abonement> { abonement }
            };
            var studentUser = new User
            {
                Id = 2,
                StudentProfile = studentProfile
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = await _iterator.GetStudentAbonements(2);

            // Assert
            var dto = result.Single();
            Assert.That(dto.StudentId, Is.EqualTo(2));
            Assert.That(dto.TeacherId, Is.EqualTo(201));
            Assert.That(dto.CourseName, Is.EqualTo("C#"));
            Assert.That(dto.CourseThemeName, Is.EqualTo("Алгоритмы"));
            Assert.That(dto.PricePerLesson, Is.EqualTo(800));
            Assert.That(dto.AbonementStatus, Is.EqualTo(StatusAbonement.Freezed));
        }

        [Test]
        public void GetStudentAbonements_WhenStudentNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetStudentAbonements(999));
        }

        #endregion

        #region DeleteAbonement

        [Test]
        public async Task DeleteAbonement_WhenValidStudentAndAbonement_UpdatesStatusAndSaves()
        {
            // Arrange
            var abonement = new Abonement { AbonementId = 303, AbonementStatus = AbonementStatus.Active };
            var studentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } };
            var studentUser = new User { Id = 3, StudentProfile = studentProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = await _iterator.DeleteAbonement(3, 303);

            // Assert
            Assert.That(result, Is.EqualTo(303));
            Assert.That(abonement.AbonementStatus, Is.EqualTo(AbonementStatus.Deleted));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void DeleteAbonement_WhenStudentNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.DeleteAbonement(999, 1));
        }

        [Test]
        public void DeleteAbonement_WhenAbonementNotFound_ThrowsException()
        {
            // Arrange
            var studentUser = new User { Id = 4, StudentProfile = new StudentProfile { Abonements = new List<Abonement>() } };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.DeleteAbonement(4, 999));
        }

        #endregion

        #region FreezeAbonement

        [Test]
        public async Task FreezeAbonement_UpdatesStatusToFreezed()
        {
            // Arrange
            var abonement = new Abonement { AbonementId = 304, AbonementStatus = AbonementStatus.Active };
            var studentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } };
            var studentUser = new User { Id = 5, StudentProfile = studentProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = await _iterator.FreezeAbonement(5, 304);

            // Assert
            Assert.That(result, Is.EqualTo(304));
            Assert.That(abonement.AbonementStatus, Is.EqualTo(AbonementStatus.Freezed));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        #endregion

        #region ActivateAbonement

        [Test]
        public async Task ActivateAbonement_UpdatesStatusAndPublishesNotification()
        {
            // Arrange
            var teacherUser = new User { Id = 202 };
            var course = new Course
            {
                CourseId = 203,
                Name = "Python",
                Teacher = new TeacherProfile { TeacherId = 202, User = teacherUser }
            };
            var abonement = new Abonement { AbonementId = 305, AbonementStatus = AbonementStatus.Freezed, Course = course };
            var studentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } };
            var studentUser = new User { Id = 6, StudentProfile = studentProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = await _iterator.ActivateAbonement(6, 305);

            // Assert
            Assert.That(result, Is.EqualTo(305));
            Assert.That(abonement.AbonementStatus, Is.EqualTo(AbonementStatus.Active));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);

            _mockPublisher.Verify(
                x => x.PublishAsync(It.Is<AbonementChangeStatusNotification>(n =>
                    n.StudentUserId == 6 &&
                    n.TeacherUserId == 202 &&
                    n.AbonementId == 305 &&
                    n.AbonementStatus == "Active"
                )),
                Times.Once);
        }

        #endregion
    }
}
