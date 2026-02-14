using CoreConnection.DTOs.Inputs;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
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
    public class PromocodeIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private PromocodeIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new PromocodeIterator(_mockContext.Object);
        }

        #region GetPromocodes

        [Test]
        public async Task GetPromocodes_ReturnsAllOrOnlyAvailable()
        {
            // Arrange
            var promocodes = new[]
            {
            new Promocode { PromocodeId = 1, Title = "Доступный", Student = null, CoinsPrice = 100 },
            new Promocode { PromocodeId = 2, Title = "Недоступный", CoinsPrice = 200, Student = new StudentProfile() }
        };

            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(promocodes);

            // Act
            var all = (await _iterator.GetPromocodes(OnlyAvailable: false)).ToList();
            var availableOnly = (await _iterator.GetPromocodes(OnlyAvailable: true)).ToList();

            // Assert
            Assert.That(all.Count, Is.EqualTo(2));
            Assert.That(availableOnly.Count, Is.EqualTo(1));
            Assert.That(availableOnly[0].Title, Is.EqualTo("Доступный"));
            Assert.That(availableOnly[0].Code, Is.Null); // по ТЗ — всегда null в этом методе
        }

        #endregion

        #region GetPromocode

        [Test]
        public async Task GetPromocode_WhenExists_ReturnsDto()
        {
            // Arrange
            var promo = new Promocode
            {
                PromocodeId = 10,
                Title = "Тестовый",
                Description = "Описание",
                CoinsPrice = 150,
                Student = null
            };

            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promo });

            // Act
            var result = await _iterator.GetPromocode(10, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.PromocodeId, Is.EqualTo(10));
                Assert.That(result.Title, Is.EqualTo("Тестовый"));
                Assert.That(result.CoinsPrice, Is.EqualTo(150));
                Assert.That(result.Code, Is.Null); // по ТЗ — всегда null
            });
        }

        [Test]
        public void GetPromocode_WhenNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new List<Promocode>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetPromocode(999, false));
        }

        [Test]
        public void GetPromocode_WhenNotAvailableAndOnlyAvailableRequested_ThrowsException()
        {
            // Arrange
            var promo = new Promocode { PromocodeId = 11, Student = new StudentProfile() };
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promo });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetPromocode(11, true));
        }

        #endregion

        #region BuyPromocode

        [Test]
        public async Task BuyPromocode_WithEnoughCoins_AssignsPromocodeToStudent()
        {
            // Arrange
            var promocode = new Promocode
            {
                PromocodeId = 20,
                Code = "SECRET123",
                CoinsPrice = 300,
                Student = null
            };
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promocode });

            var studentProfile = new StudentProfile { Coins = 500 };
            var studentUser = new User { Id = 101, StudentProfile = studentProfile };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = await _iterator.BuyPromocode(101, 20);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(studentProfile.Coins, Is.EqualTo(200)); // 500 - 300
                Assert.That(promocode.Student, Is.SameAs(studentProfile));
                Assert.That(result.Code, Is.EqualTo("SECRET123")); // при покупке код возвращается!
                Assert.That(result.PromocodeId, Is.EqualTo(20));
            });
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void BuyPromocode_WhenPromocodeNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new List<Promocode>());
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { new User { Id = 102, StudentProfile = new StudentProfile { Coins = 1000 } } });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.BuyPromocode(102, 999));
        }

        [Test]
        public void BuyPromocode_WhenStudentNotFound_ThrowsException()
        {
            // Arrange
            var promo = new Promocode { PromocodeId = 21, CoinsPrice = 100, Student = null };
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promo });
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>()); // студент не найден

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.BuyPromocode(999, 21));
        }

        [Test]
        public void BuyPromocode_WhenNotEnoughCoins_ThrowsException()
        {
            // Arrange
            var promo = new Promocode { PromocodeId = 22, CoinsPrice = 500, Student = null };
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promo });

            var studentUser = new User { Id = 103, StudentProfile = new StudentProfile { Coins = 400 } };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.BuyPromocode(103, 22));
            Assert.That(ex?.Message, Does.Contain("Not enough coins"));
        }

        #endregion

        #region GetStudentPromocodes

        [Test]
        public async Task GetStudentPromocodes_ReturnsMappedPromocodes()
        {
            // Arrange
            var promocodes = new[]
            {
            new Promocode { PromocodeId = 30, Title = "Купленный 1", Code = "A1", CoinsPrice = 100 },
            new Promocode { PromocodeId = 31, Title = "Купленный 2", Code = "B2", CoinsPrice = 200 }
        };
            var studentProfile = new StudentProfile { Promocodes = promocodes.ToList() };
            var studentUser = new User { Id = 104, StudentProfile = studentProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = (await _iterator.GetStudentPromocodes(104)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            foreach (var dto in result)
            {
                Assert.That(dto.Code, Is.Not.Null); // по ТЗ — всегда null в этом методе
            }
            Assert.That(result[0].Title, Is.EqualTo("Купленный 1"));
        }

        [Test]
        public void GetStudentPromocodes_WhenStudentNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetStudentPromocodes(999));
        }

        #endregion

        #region AddPromocode

        [Test]
        public async Task AddPromocode_WithPermission_CreatesNewPromocode()
        {
            // Arrange
            var adminUser = new User
            {
                Id = 201,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.AddPromocodes } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { adminUser });
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new List<Promocode>());

            var inputDto = new PromocodeInputDto
            {
                Code = "NEW2026",
                CoinsPrice = 250,
                Title = "Новый промокод",
                Description = "Только для вас"
            };

            // Act
            var result = await _iterator.AddPromocode(201, inputDto);

            // Assert
            _mockContext.Verify(x => x.Set<Promocode>().AddAsync(It.Is<Promocode>(p =>
                p.Code == "NEW2026" &&
                p.CoinsPrice == 250 &&
                p.Title == "Новый промокод"), It.IsAny<System.Threading.CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void AddPromocode_WhenNoPermission_ThrowsException()
        {
            // Arrange
            var adminUser = new User
            {
                Id = 202,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission>() // нет AddPromocodes
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { adminUser });

            // Act & Assert
            Assert.ThrowsAsync<NoPermissionException>(async () => await _iterator.AddPromocode(202, new PromocodeInputDto 
            { 
                Title = "Title",
                Description = "Description",
                CoinsPrice = 0,
                Code = "Code"
            }));
        }

        #endregion

        #region DeletePromocode

        [Test]
        public async Task DeletePromocode_WithPermission_RemovesAvailablePromocode()
        {
            // Arrange
            var adminUser = new User
            {
                Id = 203,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.DeletePromocodes } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { adminUser });

            var promo = new Promocode { PromocodeId = 40, Student = null };
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promo });

            // Act
            var result = await _iterator.DeletePromocode(203, 40);

            // Assert
            Assert.That(result, Is.EqualTo(40));
            _mockContext.Verify(x => x.Set<Promocode>().Remove(It.Is<Promocode>(p => p == promo)), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void DeletePromocode_WhenPromocodeSold_ThrowsException()
        {
            // Arrange
            var adminUser = new User
            {
                Id = 204,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission> { new() { Permission = Permission.DeletePromocodes } }
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { adminUser });

            var soldPromo = new Promocode { PromocodeId = 41, Student = new StudentProfile() }; // уже продан
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { soldPromo });

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.DeletePromocode(204, 41));
            Assert.That(ex?.Message, Does.Contain("was sold"));
        }

        [Test]
        public void DeletePromocode_WhenNoPermission_ThrowsException()
        {
            // Arrange
            var adminUser = new User
            {
                Id = 205,
                AdminProfile = new AdminProfile
                {
                    Permissions = new List<AdminPermission>() // нет прав
                }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { adminUser });

            var promo = new Promocode { PromocodeId = 42, Student = null };
            _mockContext.Setup(x => x.Set<Promocode>())
                .ReturnsDbSet(new[] { promo });

            // Act & Assert
            Assert.ThrowsAsync<NoPermissionException>(async () => await _iterator.DeletePromocode(205, 42));
        }

        #endregion
    }
}
