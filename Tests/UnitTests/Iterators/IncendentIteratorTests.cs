using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Iterators;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class IncidentIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private IncidentIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new IncidentIterator(_mockContext.Object);
        }

        #region GetIncidentLogs

        [Test]
        public async Task GetIncidentLogs_WithPermission_ReturnsMappedLogs()
        {
            // Arrange
            var adminUser = new User { Id = 201 };
            var adminProfile = new AdminProfile { User = adminUser };
            var log1 = new IncidentLog
            {
                LogId = 1,
                AdminProfile = adminProfile,
                DecisionDate = new DateTime(2026, 1, 30),
                Description = "Тест 1",
                IsRevisioned = false
            };
            var log2 = new IncidentLog
            {
                LogId = 2,
                AdminProfile = adminProfile,
                DecisionDate = new DateTime(2026, 1, 31),
                Description = "Тест 2",
                IsRevisioned = true
            };

            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new[] { log1, log2 });

            // Мокаем helper.CheckIteratingUser через настройку Set<User>
            var iteratingUser = new User
            {
                Id = 101,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.InspectIncidentLogs } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act
            var allLogs = (await _iterator.GetIncidentLogs(101, OnlyUnrevisioned: false)).ToList();
            var unrevisionedOnly = (await _iterator.GetIncidentLogs(101, OnlyUnrevisioned: true)).ToList();

            // Assert
            Assert.That(allLogs.Count, Is.EqualTo(2));
            Assert.That(unrevisionedOnly.Count, Is.EqualTo(1));
            Assert.That(unrevisionedOnly[0].LogId, Is.EqualTo(1));
        }

        [Test]
        public void GetIncidentLogs_WhenNoPermission_ThrowsException()
        {
            // Arrange
            var iteratingUser = new User
            {
                Id = 102,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission>() // нет InspectIncidentLogs
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act & Assert
            Assert.ThrowsAsync<NoPermissionException>(async () =>
                await _iterator.GetIncidentLogs(102, false));
        }

        #endregion

        #region GetIncidentLog

        [Test]
        public async Task GetIncidentLog_WhenExists_ReturnsDto()
        {
            // Arrange
            var adminUser = new User { Id = 202 };
            var log = new IncidentLog
            {
                LogId = 5,
                AdminProfile = new AdminProfile { User = adminUser },
                DecisionDate = DateTime.Now,
                Description = "Подробности"
            };

            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new[] { log });

            var iteratingUser = new User
            {
                Id = 103,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.InspectIncidentLogs } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act
            var result = await _iterator.GetIncidentLog(103, 5);

            // Assert
            Assert.That(result.LogId, Is.EqualTo(5));
        }

        [Test]
        public void GetIncidentLog_WhenNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new List<IncidentLog>());

            var iteratingUser = new User
            {
                Id = 104,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.InspectIncidentLogs } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetIncidentLog(104, 999));
        }

        #endregion

        #region RevisionIncidentLog

        [Test]
        public async Task RevisionIncidentLog_MarksLogAsRevisioned()
        {
            // Arrange
            var log = new IncidentLog { LogId = 10, IsRevisioned = false, AdminProfile = new AdminProfile() };
            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new[] { log });

            var iteratingUser = new User
            {
                Id = 105,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.InspectIncidentLogs } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act
            var result = await _iterator.RevisionIncidentLog(105, 10);

            // Assert
            Assert.That(result, Is.EqualTo(10));
            Assert.That(log.IsRevisioned, Is.True);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void RevisionIncidentLog_WhenLogNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new List<IncidentLog>());

            var iteratingUser = new User
            {
                Id = 106,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.InspectIncidentLogs } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { iteratingUser });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.RevisionIncidentLog(106, 999));
        }

        #endregion
    }
}
