using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Services.Iterators;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class TeacherIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private TeacherIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new TeacherIterator(_mockContext.Object);
        }

        #region GetTeachers

        [Test]
        public async Task GetTeachers_ReturnsAllOrOnlyAvailableTeachers()
        {
            // Arrange
            var availableTeacher = new User
            {
                Id = 101,
                TeacherProfile = new TeacherProfile
                {
                    About = "Опытный преподаватель",
                    ApproveStatus = ApproveStatus.Approved
                },
                IsMailChecked = true,
                IsBanned = false
            };
            var unavailableTeacher = new User
            {
                Id = 102,
                TeacherProfile = new TeacherProfile
                {
                    About = "Новый учитель",
                    ApproveStatus = ApproveStatus.NeedModeratorReview
                }
            };
            var nonTeacher = new User { Id = 103, TeacherProfile = null };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { availableTeacher, unavailableTeacher, nonTeacher });

            // Act
            var allTeachers = (await _iterator.GetTeachers(isOnlyAvailable: false)).ToList();
            var onlyAvailable = (await _iterator.GetTeachers(isOnlyAvailable: true)).ToList();

            // Assert
            Assert.That(allTeachers.Count, Is.EqualTo(2)); // nonTeacher отфильтрован
            Assert.That(onlyAvailable.Count, Is.EqualTo(1));
            Assert.That(onlyAvailable[0].IsAvailable, Is.True);
        }

        #endregion

        #region GetTeacher

        [Test]
        public async Task GetTeacher_WhenExists_ReturnsDto()
        {
            // Arrange
            var teacher = new User
            {
                Id = 201,
                TeacherProfile = new TeacherProfile
                {
                    About = "Преподаёт математику",
                    ApproveStatus = ApproveStatus.Approved
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacher });

            // Act
            var result = await _iterator.GetTeacher(201, true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.UserId, Is.EqualTo(201));
                Assert.That(result.About, Is.EqualTo("Преподаёт математику"));
                Assert.That(result.IsAvailable, Is.True);
            });
        }

        [Test]
        public void GetTeacher_WhenNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>()); // или только без нужного ID

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetTeacher(999, true));
        }

        [Test]
        public void GetTeacher_WhenUserExistsButNoTeacherProfile_ThrowsException()
        {
            // Arrange
            var nonTeacher = new User { Id = 301, TeacherProfile = null };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { nonTeacher });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetTeacher(301, true));
        }

        #endregion
    }
}
