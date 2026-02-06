using CoreConnection.DTOs;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
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
    public class UserIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private Mock<PasswordHasher> _mockPasswordHasher = null!;
        private UserIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _mockPasswordHasher = new Mock<PasswordHasher>();
            _iterator = new UserIterator(_mockContext.Object, _mockPasswordHasher.Object);
        }

        #region Login

        [Test]
        public async Task Login_WithValidCredentials_ReturnsUserId()
        {
            // Arrange
            var user = new User
            {
                Id = 101,
                MailAdress = "test@example.com",
                Password = "hashed_password",
                IsBanned = false
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            _mockPasswordHasher
                .Setup(x => x.VerifyPassword("correct_pass", "hashed_password"))
                .Returns(true);

            // Act
            var (userId, error) = await _iterator.Login("test@example.com", "correct_pass");

            // Assert
            Assert.That(userId, Is.EqualTo(101));
            Assert.That(error, Is.Empty);
        }

        [Test]
        public async Task Login_WhenUserNotFound_ReturnsUnknownLogin()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act
            var (userId, error) = await _iterator.Login("unknown@test.com", "pass");

            // Assert
            Assert.That(userId, Is.Null);
            Assert.That(error, Is.EqualTo("Unknown login"));
        }

        [Test]
        public async Task Login_WhenUserBanned_ReturnsBannedMessage()
        {
            // Arrange
            var bannedUser = new User
            {
                Id = 102,
                MailAdress = "banned@test.com",
                Password = "any",
                IsBanned = true
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { bannedUser });

            // Act
            var (userId, error) = await _iterator.Login("banned@test.com", "pass");

            // Assert
            Assert.That(userId, Is.Null);
            Assert.That(error, Is.EqualTo("User is banned"));
        }

        [Test]
        public async Task Login_WithWrongPassword_ReturnsWrongPassword()
        {
            // Arrange
            var user = new User
            {
                Id = 103,
                MailAdress = "valid@test.com",
                Password = "stored_hash",
                IsBanned = false
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            _mockPasswordHasher
                .Setup(x => x.VerifyPassword("wrong_pass", "stored_hash"))
                .Returns(false);

            // Act
            var (userId, error) = await _iterator.Login("valid@test.com", "wrong_pass");

            // Assert
            Assert.That(userId, Is.Null);
            Assert.That(error, Is.EqualTo("Wrong password"));
        }

        #endregion

        #region AddMoney

        [Test]
        public async Task AddMoney_IncreasesUserCashAndReturnsNewBalance()
        {
            // Arrange
            var user = new User { Id = 201, Cash = 1000 };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var newBalance = await _iterator.AddMoney(201, 500);

            // Assert
            Assert.That(newBalance, Is.EqualTo(1500));
            Assert.That(user.Cash, Is.EqualTo(1500));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void AddMoney_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.AddMoney(999, 100));
        }

        #endregion

        #region GetMoney

        [Test]
        public async Task GetMoney_DeductsSpecifiedAmountAndReturnsDeductedValue()
        {
            // Arrange
            var user = new User { Id = 202, Cash = 800 };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var deducted = await _iterator.GetMoney(202, 300);

            // Assert
            Assert.That(deducted, Is.EqualTo(300));
            Assert.That(user.Cash, Is.EqualTo(500));
        }

        [Test]
        public async Task GetMoney_WhenRequestedMoreThanAvailable_TakesAllAndReturnsActualAmount()
        {
            // Arrange
            var user = new User { Id = 203, Cash = 200 };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var deducted = await _iterator.GetMoney(203, 500);

            // Assert
            Assert.That(deducted, Is.EqualTo(200));
            Assert.That(user.Cash, Is.EqualTo(0));
        }

        [Test]
        public void GetMoney_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetMoney(999, 100));
        }

        #endregion

        #region RegUser

        [Test]
        public async Task RegUser_WithValidData_CreatesNewUser()
        {
            // Arrange
            var dto = new RegistrationInputDto
            {
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                MailAdress = "newuser@test.com",
                Password = "SecurePass123!"
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            _mockPasswordHasher
                .Setup(x => x.HashPassword("SecurePass123!"))
                .Returns("hashed_new_password");

            // Act
            var userId = await _iterator.RegUser(dto);

            // Assert
            _mockContext.Verify(x => x.Set<User>().Add(It.Is<User>(u =>
                u.MailAdress == "newuser@test.com" &&
                u.Password == "hashed_new_password" &&
                u.Cash == 0)), Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void RegUser_WithInvalidEmail_ThrowsException()
        {
            // Arrange
            var invalidDto = new RegistrationInputDto
            {
                MailAdress = "invalid-email",
                Password = "pass"
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.RegUser(invalidDto));
            Assert.That(ex?.Message, Does.Contain("not valid"));
        }

        [Test]
        public void RegUser_WhenUserWithEmailExists_ThrowsException()
        {
            // Arrange
            var existingUser = new User { MailAdress = "exists@test.com" };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { existingUser });

            var dto = new RegistrationInputDto
            {
                MailAdress = "exists@test.com",
                Password = "newpass"
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.RegUser(dto));
            Assert.That(ex?.Message, Does.Contain("already exists"));
        }

        #endregion

        #region CreateTeacherProfile

        [Test]
        public async Task CreateTeacherProfile_CreatesProfile_WhenUserIsEligible()
        {
            // Arrange
            var user = new User
            {
                Id = 301,
                TeacherProfile = null,
                IsBanned = false,
                IsMailChecked = true,
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var result = await _iterator.CreateTeacherProfile(301, "О себе");

            // Assert
            Assert.That(result, Is.EqualTo(301));
            Assert.That(user.TeacherProfile, Is.Not.Null);
            Assert.That(user.TeacherProfile.About, Is.EqualTo("О себе"));
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void CreateTeacherProfile_WhenUserNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.CreateTeacherProfile(999, "about"));
        }

        [Test]
        public void CreateTeacherProfile_WhenAlreadyTeacher_ThrowsException()
        {
            var user = new User 
            {   
                Id = 302, 
                TeacherProfile = new TeacherProfile(),
                IsBanned = false,
                IsMailChecked = true,
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.CreateTeacherProfile(302, "about"));
        }

        [Test]
        public void CreateTeacherProfile_WhenUserNotEnabled_ThrowsException()
        {
            var user = new User 
            {   
                Id = 303, 
                TeacherProfile = null,
                IsBanned = true,
                IsMailChecked = true,
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            var ex = Assert.ThrowsAsync<NotAvailableException>(async () => await _iterator.CreateTeacherProfile(303, "about"));
        }

        #endregion

        #region CreateStudentProfile

        [Test]
        public async Task CreateStudentProfile_CreatesProfile_WhenUserIsEligible()
        {
            // Arrange
            var user = new User
            {
                Id = 401,
                StudentProfile = null,
                IsBanned = false,
                IsMailChecked = true,
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var result = await _iterator.CreateStudentProfile(401);

            // Assert
            Assert.That(result, Is.EqualTo(401));
            Assert.That(user.StudentProfile, Is.Not.Null);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void CreateStudentProfile_WhenAlreadyStudent_ThrowsException()
        {
            var user = new User 
            {   
                Id = 402, 
                StudentProfile = new StudentProfile(),
                IsBanned = false,
                IsMailChecked = true,
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.CreateStudentProfile(402));
        }

        #endregion

        #region GetUser

        [Test]
        public async Task GetUser_WhenExists_ReturnsMappedDto()
        {
            // Arrange
            var user = new User
            {
                Id = 501,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                Cash = 2500,
                IsBanned = false,
                IsMailChecked = true,
                StudentProfile = new StudentProfile { ApproveStatus = ApproveStatus.Approved },
                TeacherProfile = null,
                AdminProfile = new AdminProfile()
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            // Act
            var dto = await _iterator.GetUser(501, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(501));
                Assert.That(dto.Name, Is.EqualTo("Анна"));
                Assert.That(dto.Cash, Is.EqualTo(2500));
                Assert.That(dto.IsApprovedStudent, Is.True);
                Assert.That(dto.IsApprovedTeacher, Is.Null);
                Assert.That(dto.IsAdmin, Is.True);
                Assert.That(dto.IsBanned, Is.False);
                Assert.That(dto.MailConfirmed, Is.True);
            });
        }

        [Test]
        public void GetUser_WhenNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetUser(999, false));
        }

        [Test]
        public void GetUser_WhenNotEnable_ThrowsException()
        {
            var user = new User
            {
                Id = 501,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                Cash = 2500,
                IsBanned = true,
                IsMailChecked = false,
                StudentProfile = new StudentProfile { ApproveStatus = ApproveStatus.Approved },
                TeacherProfile = null,
                AdminProfile = new AdminProfile()
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetUser(999, true));
        }

        #endregion

        #region GetUsers

        [Test]
        public async Task GetUsers_WhenAll_ReturnsMappedDto()
        {
            // Arrange
            var user1 = new User
            {
                Id = 501,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                Cash = 2500,
                IsBanned = false,
                IsMailChecked = true,
                StudentProfile = new StudentProfile { ApproveStatus = ApproveStatus.Approved },
                TeacherProfile = null,
                AdminProfile = new AdminProfile()
            };
            var user2 = new User
            {
                Id = 501,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                Cash = 2500,
                IsBanned = true,
                IsMailChecked = false,
                StudentProfile = new StudentProfile { ApproveStatus = ApproveStatus.Approved },
                TeacherProfile = null,
                AdminProfile = new AdminProfile()
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user1, user2 });

            // Act
            var dtos = await _iterator.GetUsers(false);

            // Assert
            Assert.That(dtos.Count() == 2);
        }

        [Test]
        public async Task GetUsers_WhenOnlyAvailable_ReturnsNothing()
        {
            // Arrange
            var user1 = new User
            {
                Id = 501,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                Cash = 2500,
                IsBanned = false,
                IsMailChecked = true,
                StudentProfile = new StudentProfile { ApproveStatus = ApproveStatus.Approved },
                TeacherProfile = null,
                AdminProfile = new AdminProfile()
            };
            var user2 = new User
            {
                Id = 501,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                Cash = 2500,
                IsBanned = true,
                IsMailChecked = false,
                StudentProfile = new StudentProfile { ApproveStatus = ApproveStatus.Approved },
                TeacherProfile = null,
                AdminProfile = new AdminProfile()
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user1, user2 });

            // Act
            var dtos = await _iterator.GetUsers(true);

            // Assert
            Assert.That(dtos.Count() == 1);
        }

        #endregion
    }
}
