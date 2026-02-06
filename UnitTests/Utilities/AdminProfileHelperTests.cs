using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Utilities
{
    public class AdminProfileHelperTests
    {
        private Mock<IPrimumContext> _mockContext;
        private AdminProfileHelper _helper;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _helper = new AdminProfileHelper(_mockContext.Object);
        }

        #region GetIteratingUser

        [Test]
        public async Task GetIteratingUser_WhenUserAndAdminProfileExist_ReturnsUser()
        {
            var user = new User
            {
                Id = 100,
                AdminProfile = new AdminProfile
                {
                    UserId = 100,
                    Permissions = new List<AdminPermission>(),
                    GivenPermissions = new List<AdminPermission>()
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            var result = await _helper.GetIteratingUser(100);

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void GetIteratingUser_WhenUserNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _helper.GetIteratingUser(999));
        }

        [Test]
        public void GetIteratingUser_WhenAdminProfileIsNull_ThrowsException()
        {
            var user = new User { Id = 100, AdminProfile = null };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _helper.GetIteratingUser(100));
        }

        #endregion

        #region CheckIteratingUser

        [Test]
        public async Task CheckIteratingUser_WhenPermissionGranted_ReturnsUser()
        {
            var user = new User
            {
                Id = 100,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission>
                {
                    new() { Permission = Permission.AddCash }
                },
                    GivenPermissions = new List<AdminPermission>()
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            var result = await _helper.CheckIteratingUser(100, Permission.AddCash);

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void CheckIteratingUser_WhenPermissionNotGranted_ThrowsException()
        {
            var user = new User
            {
                Id = 100,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission>
                {
                    new() { Permission = Permission.EditPermissions }
                },
                    GivenPermissions = new List<AdminPermission>()
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            Assert.ThrowsAsync<NoPermissionException>(async () =>
                await _helper.CheckIteratingUser(100, Permission.AddCash));
        }

        [Test]
        public void CheckIteratingUser_WhenUserNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _helper.CheckIteratingUser(999, Permission.AddCash));
        }

        #endregion

        #region GetAllPermissions

        [Test]
        public void GetAllPermissions_WhenAdminHasSomePermissions_ReturnsCorrectMap()
        {
            var admin = new AdminProfile
            {
                Permissions = new List<AdminPermission>
            {
                new() { Permission = Permission.AddCash },
                new() { Permission = Permission.DeleteLessons }
            }
            };

            var result = _helper.GetAllPermissions(admin);

            Assert.That(result.Count, Is.EqualTo(Enum.GetValues<Permission>().Length));
            Assert.That(result[nameof(Permission.AddCash)], Is.True);
            Assert.That(result[nameof(Permission.DeleteLessons)], Is.True);
            Assert.That(result[nameof(Permission.EditPermissions)], Is.False);
        }

        [Test]
        public void GetAllPermissions_WhenAdminHasNoPermissions_ReturnsAllFalse()
        {
            var admin = new AdminProfile
            {
                Permissions = new List<AdminPermission>()
            };

            var result = _helper.GetAllPermissions(admin);

            Assert.That(result.Values.All(v => v == false), Is.True);
        }

        #endregion
    }
}
