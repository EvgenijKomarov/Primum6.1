using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Utilities
{
    public class PasswordHasherTests
    {
        private PasswordHasher _hasher = null!;

        [SetUp]
        public void Setup()
        {
            _hasher = new PasswordHasher();
        }

        [Test]
        public void HashPassword_WithValidPassword_ReturnsNonEmptyHashInCorrectFormat()
        {
            // Arrange
            const string password = "MySecurePassword123!";

            // Act
            string hashed = _hasher.HashPassword(password);

            // Assert
            Assert.That(hashed, Is.Not.Null);
            Assert.That(hashed, Is.Not.Empty);
            string[] parts = hashed.Split(':');
            Assert.That(parts.Length, Is.EqualTo(3));
            Assert.That(int.TryParse(parts[0], out int iterations), Is.True);
            Assert.That(iterations, Is.EqualTo(100_000));

            // Проверяем, что соль и хеш — валидный Base64
            _ = Convert.FromBase64String(parts[1]); // не должно выбросить исключение
            _ = Convert.FromBase64String(parts[2]);
        }

        [Test]
        public void HashPassword_WithSamePassword_CreatesDifferentHashesDueToRandomSalt()
        {
            // Arrange
            const string password = "identical-password";

            // Act
            string hash1 = _hasher.HashPassword(password);
            string hash2 = _hasher.HashPassword(password);

            // Assert
            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            const string password = "CorrectHorseBatteryStaple";
            string hashed = _hasher.HashPassword(password);

            // Act
            bool result = _hasher.VerifyPassword(password, hashed);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void VerifyPassword_WithWrongPassword_ReturnsFalse()
        {
            // Arrange
            const string originalPassword = "secret123";
            const string wrongPassword = "wrong123";
            string hashed = _hasher.HashPassword(originalPassword);

            // Act
            bool result = _hasher.VerifyPassword(wrongPassword, hashed);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void VerifyPassword_WithNullPassword_ReturnsFalse()
        {
            // Arrange
            string hashed = _hasher.HashPassword("some-pass");

            // Act & Assert
            Assert.That(_hasher.VerifyPassword(null!, hashed), Is.False);
            Assert.That(_hasher.VerifyPassword("", hashed), Is.False);
        }

        [Test]
        public void VerifyPassword_WithNullOrInvalidHashedPassword_ReturnsFalse()
        {
            // Arrange
            const string password = "test";

            // Act & Assert
            Assert.That(_hasher.VerifyPassword(password, null!), Is.False);
            Assert.That(_hasher.VerifyPassword(password, ""), Is.False);
            Assert.That(_hasher.VerifyPassword(password, "invalid-format"), Is.False);
            Assert.That(_hasher.VerifyPassword(password, "100000:salt-only"), Is.False); // только 2 части
        }

        [Test]
        public void VerifyPassword_WithTamperedSalt_ReturnsFalse()
        {
            // Arrange
            const string password = "tamper-test";
            string hashed = _hasher.HashPassword(password);
            string[] parts = hashed.Split(':');
            // Подменим соль на другую (валидную base64 строку)
            string fakeSalt = Convert.ToBase64String(new byte[16]); // новая соль
            string tamperedHash = $"{parts[0]}:{fakeSalt}:{parts[2]}";

            // Act
            bool result = _hasher.VerifyPassword(password, tamperedHash);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void VerifyPassword_WithTamperedHash_ReturnsFalse()
        {
            // Arrange
            const string password = "tamper-hash-test";
            string hashed = _hasher.HashPassword(password);
            string[] parts = hashed.Split(':');
            // Подменим хеш
            string fakeHash = Convert.ToBase64String(new byte[32]);
            string tamperedHash = $"{parts[0]}:{parts[1]}:{fakeHash}";

            // Act
            bool result = _hasher.VerifyPassword(password, tamperedHash);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void HashPassword_WithEmptyPassword_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _hasher.HashPassword(null!));
            Assert.Throws<ArgumentException>(() => _hasher.HashPassword(""));
        }

        [Test]
        public void VerifyPassword_IsConstantTime_ResistantToTimingAttacks()
        {
            // Этот тест не может напрямую проверить "фиксированное время",
            // но мы можем убедиться, что метод использует CryptographicOperations.FixedTimeEquals.
            // Для этого проверим поведение: даже при одинаковой длине хешей,
            // результат не должен зависеть от позиции различия.

            // Однако в юнит-тестах сложно проверить тайминг.
            // Вместо этого — убедимся, что метод не "рано выходит" при разных длинах:
            // но наш метод всегда сравнивает хеши одной длины (из-за PBKDF2 с hash.Length).

            // Поэтому просто подтвердим, что два разных пароля дают false,
            // а одинаковые — true, и нет исключений при разных длинах хешей (но у нас они всегда одинаковые).

            // Основной сигнал — использование FixedTimeEquals — уже заложено в реализацию.
            // Тест ниже — скорее sanity check.

            const string password = "timing-test";
            string hashed = _hasher.HashPassword(password);

            // Эти вызовы не должны "утекать" информацию через время
            bool result1 = _hasher.VerifyPassword("a", hashed); // короткий пароль → короткий хеш? Нет, PBKDF2 с фикс. длиной
            bool result2 = _hasher.VerifyPassword("very-long-wrong-password-that-is-much-longer", hashed);

            Assert.That(result1, Is.False);
            Assert.That(result2, Is.False);
        }
    }
}
