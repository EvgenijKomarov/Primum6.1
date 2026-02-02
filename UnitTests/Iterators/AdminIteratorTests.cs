using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class AdminIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private AdminIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new AdminIterator(_mockContext.Object);
        }

        #region GetAdmins

        [Test]
        public async Task GetAdmins_ReturnsAllAdminsMappedToDto()
        {
            // Arrange
            var admin1 = new User
            {
                Id = 101,
                AdminProfile = new AdminProfile
                {
                    Status = "Senior",
                    Permissions = new List<AdminPermission>
                    {
                        new() { Permission = Permission.ModerateCourses }
                    }
                }
            };
            var admin2 = new User
            {
                Id = 102,
                AdminProfile = new AdminProfile
                {
                    Status = "Junior",
                    Permissions = new List<AdminPermission>()
                }
            };
            var nonAdmin = new User { Id = 103, AdminProfile = null };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { admin1, admin2, nonAdmin });

            // Act
            var result = await _iterator.GetAdmins();

            // Assert
            var list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(2));

            var ivan = list.First(x => x.UserId == 101);
            Assert.That(ivan.Status, Is.EqualTo("Senior"));
            Assert.That(ivan.Permissions["ModerateCourses"], Is.True);
            Assert.That(ivan.Permissions["ApproveCourses"], Is.False); // должно быть false по умолчанию

            var anna = list.First(x => x.UserId == 102);
            Assert.That(anna.Permissions.Values.All(v => v == false), Is.True);
        }

        #endregion

        #region GetAdmin

        [Test]
        public async Task GetAdmin_WhenAdminExists_ReturnsDto()
        {
            // Arrange
            var admin = new User
            {
                Id = 201,
                AdminProfile = new AdminProfile
                {
                    Status = "Lead",
                    Permissions = new List<AdminPermission>
                {
                    new() { Permission = Permission.EditPermissions }
                }
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { admin });

            // Act
            var result = await _iterator.GetAdmin(201);

            // Assert
            Assert.That(result.UserId, Is.EqualTo(201));
            Assert.That(result.Permissions["EditPermissions"], Is.True);
        }

        [Test]
        public void GetAdmin_WhenAdminNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetAdmin(999));
        }

        #endregion

        #region AddCash

        [Test]
        public async Task AddCash_WithValidPermission_AddsCashAndLogsIncident()
        {
            // Arrange
            var iteratingAdminProfile = new AdminProfile
            {
                AdminId = 1,
                Permissions = new List<AdminPermission> { new() { Permission = Permission.AddCash } },
                IncidentLogs = new List<IncidentLog>()
            };
            var iteratingUser = new User { Id = 100, AdminProfile = iteratingAdminProfile };

            var targetUser = new User { Id = 200, Cash = 500 };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, targetUser });

            // Act
            var result = await _iterator.AddCash(100, 200, 300);

            // Assert
            Assert.That(result, Is.EqualTo(200));
            Assert.That(targetUser.Cash, Is.EqualTo(800));
            Assert.That(iteratingAdminProfile.IncidentLogs.Count, Is.EqualTo(1));
            Assert.That(iteratingAdminProfile.IncidentLogs.First().Description, Does.Contain("Added cash (300 to userId 200)"));

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void AddCash_WhenNoPermission_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 101,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission>() // нет AddCash
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _iterator.AddCash(101, 201, 100));
            Assert.That(ex?.Message, Does.Contain("permission policy"));
        }

        [Test]
        public void AddCash_WhenTargetUserNotFound_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 102,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.AddCash } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser }); // целевого пользователя нет

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.AddCash(102, 999, 100));
        }

        #endregion

        #region EditPermissions

        [Test]
        public async Task EditPermissions_GivesAndTakesPermissionsCorrectly()
        {
            // Arrange
            var iteratingAdminProfile = new AdminProfile
            {
                AdminId = 1,
                Permissions = new List<AdminPermission> { new() { Permission = Permission.EditPermissions } },
                IncidentLogs = new List<IncidentLog>()
            };
            var iteratingUser = new User { Id = 300, AdminProfile = iteratingAdminProfile };

            var targetAdminProfile = new AdminProfile
            {
                Permissions = new List<AdminPermission>
            {
                new() { Permission = Permission.ModerateCourses } // уже есть
            }
            };
            var targetUser = new User { Id = 400, AdminProfile = targetAdminProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, targetUser });
            _mockContext.Setup(x => x.Set<AdminPermission>())
                .ReturnsDbSet(new List<AdminPermission>());

            var editedPermissions = new Dictionary<string, bool>
            {
                ["ModerateCourses"] = false,     // забрать
                ["ApproveCourses"] = true        // дать
            };

            // Act
            var result = await _iterator.EditPermissions(300, 400, editedPermissions);

            // Assert
            Assert.That(result, Is.EqualTo(400));

            // Проверяем, что старое разрешение удалено
            Assert.That(targetAdminProfile.Permissions.Any(p => p.Permission == Permission.ModerateCourses), Is.False);

            // Проверяем, что новое добавлено
            var newPerm = targetAdminProfile.Permissions.FirstOrDefault(p => p.Permission == Permission.ApproveCourses);
            Assert.That(newPerm, Is.Not.Null);
            Assert.That(newPerm!.PromoterAdminProfile, Is.SameAs(targetAdminProfile)); // ← возможно, ошибка в логике (см. примечание ниже)

            // Логирование
            Assert.That(iteratingAdminProfile.IncidentLogs.Count, Is.EqualTo(1));
            Assert.That(iteratingAdminProfile.IncidentLogs.First().Description, Does.Contain("Given permissions"));
            Assert.That(iteratingAdminProfile.IncidentLogs.First().Description, Does.Contain("Taken permissions"));

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void EditPermissions_WhenTargetIsNotAdmin_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 301,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.EditPermissions } }
                }
            };
            var nonAdmin = new User { Id = 401, AdminProfile = null };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, nonAdmin });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.EditPermissions(301, 401, new()));
        }

        #endregion

        #region CreateAdminProfile

        [Test]
        public async Task CreateAdminProfile_CreatesProfileWhenNotExists()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 500,
                AdminProfile = new AdminProfile
                {
                    AdminId = 1,
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.CreateAdminProfiles } },
                    IncidentLogs = new List<IncidentLog>()
                }
            };
            var targetUser = new User { Id = 600, AdminProfile = null };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, targetUser });

            // Act
            var result = await _iterator.CreateAdminProfile(500, 600, "Newbie");

            // Assert
            Assert.That(result, Is.EqualTo(600));
            Assert.That(targetUser.AdminProfile, Is.Not.Null);
            Assert.That(targetUser.AdminProfile!.Status, Is.EqualTo("Newbie"));

            Assert.That(iteratingUser.AdminProfile!.IncidentLogs.Count, Is.EqualTo(1));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void CreateAdminProfile_WhenAlreadyExists_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 501,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.CreateAdminProfiles } }
                }
            };
            var existingAdmin = new User { Id = 601, AdminProfile = new AdminProfile() };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, existingAdmin });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.CreateAdminProfile(501, 601, "Test"));
        }

        #endregion

        #region DeleteAdminProfile

        [Test]
        public async Task DeleteAdminProfile_RemovesProfileAndPermissions()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 700,
                AdminProfile = new AdminProfile
                {
                    AdminId = 1,
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.CreateAdminProfiles } },
                    IncidentLogs = new List<IncidentLog>()
                }
            };
            var targetAdminProfile = new AdminProfile
            {
                Permissions = new List<AdminPermission>
                {
                    new(), new()
                }
            };
            var targetUser = new User { Id = 800, AdminProfile = targetAdminProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, targetUser });
            _mockContext.Setup(x => x.Set<AdminPermission>())
                .ReturnsDbSet(targetAdminProfile.Permissions);
            _mockContext.Setup(x => x.Set<AdminProfile>())
                .ReturnsDbSet(new[] { targetAdminProfile });

            // Act
            var result = await _iterator.DeleteAdminProfile(700, 800);

            // Assert
            Assert.That(result, Is.EqualTo(800));

            _mockContext.Verify(x => x.Set<AdminPermission>().RemoveRange(It.Is<IEnumerable<AdminPermission>>(list =>
                list.Count() == 2)), Times.Once);
            _mockContext.Verify(x => x.Set<AdminProfile>().Remove(It.Is<AdminProfile>(p => p == targetAdminProfile)), Times.Once);

            Assert.That(iteratingUser.AdminProfile!.IncidentLogs.Count, Is.EqualTo(1));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void DeleteAdminProfile_WhenNoAdminProfile_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 701,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.CreateAdminProfiles } }
                }
            };
            var nonAdmin = new User { Id = 801, AdminProfile = null };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, nonAdmin });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.DeleteAdminProfile(701, 801));
        }

        #endregion

        #region BanUser

        [Test]
        public void BanUser_WhenNoUser_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 701,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.BanUsers } }
                }
            };
            var user = new User { Id = 800, IsBanned = false };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, user });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.BanUser(701, 801));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task BanUser_WhenUserExists_UserBanned()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 701,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.BanUsers } }
                }
            };
            var user = new User { Id = 801, IsBanned = false };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, user });

            // Act 
            await _iterator.BanUser(701, 801);

            //Assert
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
            Assert.That(user.IsBanned == true);
        }

        #endregion

        #region UnbanUser

        [Test]
        public void UnbanUser_WhenNoUser_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 701,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.UnbanUsers } }
                }
            };
            var user = new User { Id = 801, IsBanned = true };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, user });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.UnbanUser(701, 800));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task UnbanUser_WhenUserExists_UserUnbanned()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 701,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.UnbanUsers } }
                }
            };
            var user = new User { Id = 801, IsBanned = true };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser, user });

            // Act 
            await _iterator.UnbanUser(701, 801);

            //Assert
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
            Assert.That(user.IsBanned == false);
        }

        #endregion
    }
}
