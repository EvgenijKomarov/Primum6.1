using CoreConnection.Notifications;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Iterators;
using PrimumCore.Services.Utilities;

namespace UnitTests.Iterators
{
    public class TokenIteratorTests
    {
        private Mock<IPrimumContext> _mockContext;
        private Mock<RandomStringGenerator> _mockRandomGenerator;
        private Mock<IPublisher> _mockPublisher;
        private TokenIterator _service;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _mockRandomGenerator = new Mock<RandomStringGenerator>();
            _mockPublisher = new Mock<IPublisher>();

            _service = new TokenIterator(_mockContext.Object, _mockRandomGenerator.Object, _mockPublisher.Object);
        }

        #region SendEmailVerification

        [Test]
        public async Task SendEmailVerification_WhenEmailChanged_TokenCreatedAndEmailSent()
        {
            // Arrange
            var userId = 123;
            var email = "user@example.com";
            var newEmail = "new@example.com";
            var tokenValue = "abc123xyz";

            var user = new User
            {
                Id = userId,
                MailAdress = email,
                VerificationTokens = new List<VerificationToken>()
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });
            _mockContext.Setup(x => x.Set<VerificationToken>())
                .ReturnsDbSet(new List<VerificationToken>());

            _mockRandomGenerator.Setup(x => x.GenerateRandomString(It.IsAny<int>()))
                .Returns(tokenValue);

            // Act
            var result = await _service.SendEmailVerification(userId, newEmail);

            // Assert
            Assert.That(result, Is.EqualTo(userId));
            Assert.That(user.MailAdress, Is.EqualTo(newEmail));
            Assert.That(user.VerificationTokens.Count, Is.EqualTo(1));

            var createdToken = user.VerificationTokens.First();
            Assert.That(createdToken.Token, Is.EqualTo(tokenValue));
            Assert.That(createdToken.Meaning, Is.EqualTo(TokenMeaning.EmailVerification));
            Assert.That(createdToken.LifeTime, Is.GreaterThan(DateTime.Now.AddHours(11))); // ~12h

            _mockPublisher.Verify(x => x.PublishAsync(It.Is<UserVerificationNotification>(n =>
                n.EmailAdress == newEmail &&
                n.VerificationHash == tokenValue &&
                n.Userid == userId)), Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task SendEmailVerification_WhenEmailNotChanged_TokenCreatedAndEmailSent()
        {
            // Arrange
            var userId = 123;
            var email = "user@example.com";
            var tokenValue = "abc123xyz";

            var user = new User
            {
                Id = userId,
                MailAdress = email,
                VerificationTokens = new List<VerificationToken>()
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });
            _mockContext.Setup(x => x.Set<VerificationToken>())
                .ReturnsDbSet(new List<VerificationToken>());

            _mockRandomGenerator.Setup(x => x.GenerateRandomString(It.IsAny<int>()))
                .Returns(tokenValue);

            // Act
            var result = await _service.SendEmailVerification(userId, null);

            // Assert
            Assert.That(result, Is.EqualTo(userId));
            Assert.That(user.MailAdress, Is.EqualTo(email));
            Assert.That(user.VerificationTokens.Count, Is.EqualTo(1));

            var createdToken = user.VerificationTokens.First();
            Assert.That(createdToken.Token, Is.EqualTo(tokenValue));
            Assert.That(createdToken.Meaning, Is.EqualTo(TokenMeaning.EmailVerification));
            Assert.That(createdToken.LifeTime, Is.GreaterThan(DateTime.Now.AddHours(11))); // ~12h

            _mockPublisher.Verify(x => x.PublishAsync(It.Is<UserVerificationNotification>(n =>
                n.EmailAdress == email &&
                n.VerificationHash == tokenValue &&
                n.Userid == userId)), Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void SendEmailVerification_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _service.SendEmailVerification(999, null));
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }

        #endregion

        #region ConfirmToken

        [Test]
        public async Task ConfirmToken_WhenValidTokenProvided_MarksAsUsedAndVerifiesEmail()
        {
            // Arrange
            var userId = 123;
            var tokenStr = "valid_token_123";
            var user = new User
            {
                Id = userId,
                MailAdress = "user@example.com",
                IsMailChecked = false,
                VerificationTokens = new List<VerificationToken>
            {
                new VerificationToken
                {
                    Token = tokenStr,
                    LifeTime = DateTime.Now.AddHours(1),
                    Meaning = TokenMeaning.EmailVerification,
                    IsUsed = false
                }
            }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            // Act
            var result = await _service.ConfirmToken(userId, tokenStr);

            // Assert
            Assert.That(result, Is.EqualTo(userId));
            Assert.That(user.IsMailChecked, Is.True);
            Assert.That(user.VerificationTokens.First().IsUsed, Is.True);
            _mockPublisher.Verify(x => x.PublishAsync(It.Is<UserVerifiedEmailNotification>(n =>
                n.EmailAdress == user.MailAdress &&
                n.Userid == userId)), Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ConfirmToken_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _service.ConfirmToken(999, "any"));
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }

        [Test]
        public void ConfirmToken_WhenTokenNotFound_ThrowsException()
        {
            // Arrange
            var user = new User
            {
                Id = 123,
                VerificationTokens = new List<VerificationToken>()
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _service.ConfirmToken(123, "nonexistent"));
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }

        [Test]
        public void ConfirmToken_WhenTokenExpired_ThrowsException()
        {
            // Arrange
            var user = new User
            {
                Id = 123,
                VerificationTokens = new List<VerificationToken>
            {
                new VerificationToken
                {
                    Token = "old_token",
                    LifeTime = DateTime.Now.AddHours(-1),
                    IsUsed = false,
                    Meaning = TokenMeaning.EmailVerification
                }
            }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            // Act & Assert
            Assert.ThrowsAsync<BusinessLogicException>(async () =>
                await _service.ConfirmToken(123, "old_token"));
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }

        [Test]
        public void ConfirmToken_WhenTokenAlreadyUsed_ThrowsException()
        {
            // Arrange
            var user = new User
            {
                Id = 123,
                VerificationTokens = new List<VerificationToken>
            {
                new VerificationToken
                {
                    Token = "used_token",
                    LifeTime = DateTime.Now.AddHours(1),
                    IsUsed = true,
                    Meaning = TokenMeaning.EmailVerification
                }
            }
            };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });

            // Act & Assert
            Assert.ThrowsAsync<BusinessLogicException>(async () =>
                await _service.ConfirmToken(123, "used_token"));
            _mockPublisher.Verify(x => x.PublishAsync(It.IsAny<INotification>()), Times.Never);
        }

        #endregion
    }
}