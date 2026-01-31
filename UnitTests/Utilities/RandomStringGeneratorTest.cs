using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Utilities
{
    public class RandomStringGeneratorTests
    {
        private RandomStringGenerator _generator;

        [SetUp]
        public void Setup()
        {
            _generator = new RandomStringGenerator();
        }

        [Test]
        public void GenerateRandomString_ReturnsStringOfLength10()
        {
            // Act
            var result = _generator.GenerateRandomString();

            // Assert
            Assert.That(result, Has.Length.EqualTo(10));
        }

        [Test]
        public void GenerateRandomString_ReturnsOnlyLowercaseLetters()
        {
            // Act
            var result = _generator.GenerateRandomString();

            // Assert
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(result.All(c => c >= 'a' && c <= 'z'), Is.True,
                $"String '{result}' contains non-lowercase-letter characters.");
        }

        [Test]
        public void GenerateRandomString_DoesNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _generator.GenerateRandomString());
        }

        [Test]
        public void GenerateRandomString_GeneratesDifferentValuesOverMultipleCalls()
        {
            // Arrange
            var results = new HashSet<string>();

            // Act
            for (int i = 0; i < 100; i++)
            {
                results.Add(_generator.GenerateRandomString());
            }

            // Assert
            // С вероятностью ~100% хотя бы некоторые строки будут разными
            // (пространство 26^10 ≈ 1.4e14, коллизии за 100 попыток крайне маловероятны)
            Assert.That(results.Count, Is.GreaterThanOrEqualTo(95),
                "Too many duplicates — possible bug in randomness or logic.");
        }
    }
}
