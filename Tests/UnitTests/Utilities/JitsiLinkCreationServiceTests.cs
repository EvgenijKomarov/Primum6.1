using Microsoft.Extensions.Logging;
using Moq;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Utilities
{
    public class JitsiLinkCreationServiceTests
    {
        [Test]
        public void CreateJitsiMeeting_WithValidInput_ReturnsCorrectLinks()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<JitsiLinkCreationService>>();
            var service = new JitsiLinkCreationService(loggerMock.Object);
            const string input = "test-room-123";

            // Act
            var (adminLink, guestLink) = service.CreateJitsiMeeting(input);

            // Assert
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            string expectedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            string expectedAdmin = $"https://meet.jit.si/{expectedHash}?userType=admin";
            string expectedGuest = $"https://meet.jit.si/{expectedHash}?userType=guest";

            Assert.That(adminLink, Is.EqualTo(expectedAdmin));
            Assert.That(guestLink, Is.EqualTo(expectedGuest));
            Assert.That(adminLink, Is.Not.EqualTo(guestLink));
            Assert.That(adminLink, Does.Contain("userType=admin"));
            Assert.That(guestLink, Does.Contain("userType=guest"));
        }

        [Test]
        public void CreateJitsiMeeting_DifferentInputs_ProduceDifferentHashes()
        {
            // Arrange
            var service = new JitsiLinkCreationService();
            const string input1 = "room-A";
            const string input2 = "room-B";

            // Act
            var (admin1, _) = service.CreateJitsiMeeting(input1);
            var (admin2, _) = service.CreateJitsiMeeting(input2);

            // Assert
            Assert.That(admin1, Is.Not.EqualTo(admin2));
        }

        [Test]
        public void CreateJitsiMeeting_EmptyInput_GeneratesValidLink()
        {
            // Arrange
            var service = new JitsiLinkCreationService();
            const string input = "";

            // Act
            var (adminLink, guestLink) = service.CreateJitsiMeeting(input);

            // Assert
            const string emptyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            Assert.That(adminLink, Does.Contain(emptyHash));
            Assert.That(guestLink, Does.Contain(emptyHash));
        }

        [Test]
        public void CreateJitsiMeeting_WithLogger_LogsInformation()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<JitsiLinkCreationService>>();
            var service = new JitsiLinkCreationService(loggerMock.Object);
            const string input = "logged-room";

            // Act
            var _ = service.CreateJitsiMeeting(input);

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Creating links for ({input})")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Links creation successful")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        // Вспомогательный класс для тестирования исключений
        private class FaultyJitsiLinkCreationService : JitsiLinkCreationService
        {
            public FaultyJitsiLinkCreationService(ILogger<JitsiLinkCreationService> logger) : base(logger) { }

            public override (string adminLink, string guestLink) CreateJitsiMeeting(string input)
            {
                throw new InvalidOperationException("Simulated failure");
            }
        }
    }
}
