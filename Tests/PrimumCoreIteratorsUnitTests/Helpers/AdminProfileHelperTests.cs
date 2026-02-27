using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    public class AdminProfileHelperTests
    {
        private Mock<PrimumContext> _mockContext = null!;
        private AdminProfileHelper _helper = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<PrimumContext>();
            _helper = new AdminProfileHelper(_mockContext.Object);
        }

        #region GetIteratingUser

        [Test]
        public async Task GetIteratingUser_WhenUserAndAdminProfileExist_ReturnsUser()
        {
            // Arrange
            var adminProfile = new AdminProfile
            {
                AdminId = 10,
                Permissions = new List<AdminPermission>
            {
                new() { Permission = Permission.ModerateCourses },
                new() { Permission = Permission.AdministrateStudents }
            },
                GivenPermissions = new List<AdminPermission>() // может быть пустой
            };
            var user = new User
            {
                Id = 100,
                AdminProfile = adminProfile
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var result = await _helper.GetIteratingUser(100);

            // Assert
            Assert.That(result, Is.SameAs(user));
            Assert.That(result.AdminProfile, Is.Not.Null);
            Assert.That(result.AdminProfile.Permissions.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetIteratingUser_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _helper.GetIteratingUser(999));
        }

        [Test]
        public void GetIteratingUser_WhenUserExistsButNoAdminProfile_ThrowsException()
        {
            // Arrange
            var userWithoutAdmin = new User { Id = 200, AdminProfile = null };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { userWithoutAdmin });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _helper.GetIteratingUser(200));
        }

        #endregion

        #region CheckIteratingUser

        [Test]
        public async Task CheckIteratingUser_WhenUserHasRequiredPermission_ReturnsUser()
        {
            // Arrange
            var adminProfile = new AdminProfile
            {
                AdminId = 11,
                Permissions = new List<AdminPermission>
            {
                new() { Permission = Permission.ModerateTeachers }
            }
            };
            var user = new User { Id = 101, AdminProfile = adminProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var result = await _helper.CheckIteratingUser(101, Permission.ModerateTeachers);

            // Assert
            Assert.That(result, Is.SameAs(user));
        }

        [Test]
        public void CheckIteratingUser_WhenUserDoesNotHaveRequiredPermission_ThrowsException()
        {
            // Arrange
            var adminProfile = new AdminProfile
            {
                AdminId = 12,
                Permissions = new List<AdminPermission>
            {
                new() { Permission = Permission.ModerateCourses } // нет ModerateTeachers
            }
            };
            var user = new User { Id = 102, AdminProfile = adminProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act & Assert
            var ex = Assert.ThrowsAsync<NoPermissionException>(async () =>
                await _helper.CheckIteratingUser(102, Permission.ModerateTeachers));
        }

        [Test]
        public void CheckIteratingUser_WhenUserNotFound_ThrowsExceptionFromGetIteratingUser()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _helper.CheckIteratingUser(999, Permission.ApproveCourses));
        }

        #endregion

        #region GetAllPermissions

        [Test]
        public void GetAllPermissions_ReturnsDictionaryWithAllPermissionsAndTheirStatus()
        {
            // Arrange
            var adminProfile = new AdminProfile
            {
                Permissions = new List<AdminPermission>
            {
                new() { Permission = Permission.ModerateCourses },
                new() { Permission = Permission.ApproveCourses }
            }
            };

            // Act
            var result = _helper.GetAllPermissions(adminProfile);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(Enum.GetValues<Permission>().Length));

            // Проверяем конкретные значения
            Assert.That(result["ModerateCourses"], Is.True);
            Assert.That(result["ApproveCourses"], Is.True);
            Assert.That(result["AdministrateTeachers"], Is.False); // предполагаем, что его нет
            Assert.That(result.ContainsKey("EditPermissions"), Is.True); // должен быть ключ для каждого enum-значения
        }

        [Test]
        public void GetAllPermissions_WhenNoPermissions_ReturnsAllFalse()
        {
            // Arrange
            var adminProfile = new AdminProfile { Permissions = new List<AdminPermission>() };

            // Act
            var result = _helper.GetAllPermissions(adminProfile);

            // Assert
            Assert.That(result.Values.All(v => v == false), Is.True);
        }

        #endregion
    }
}
