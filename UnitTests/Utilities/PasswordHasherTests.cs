using NUnit.Framework;
using PrimumCore.Services.Utilities;

namespace UnitTests.Utilities
{
    public class PasswordHasherTests
    {
        private PasswordHasher _hasher;

        [SetUp]
        public void Setup()
        {
            _hasher = new PasswordHasher();
        }

        [Test]
        public void HashAndVerify_WorksForCorrectPassword()
        {
            var hashed = _hasher.HashPassword("MySecret123!");
            Assert.That(hashed, Is.Not.Null.And.Not.Empty);

            var ok = _hasher.VerifyPassword("MySecret123!", hashed);
            Assert.That(ok, Is.True);

            var wrong = _hasher.VerifyPassword("BadPassword", hashed);
            Assert.That(wrong, Is.False);
        }

        [Test]
        public void Hash_EmptyPassword_Throws()
        {
            Assert.Throws<System.ArgumentException>(() => _hasher.HashPassword(string.Empty));
        }
    }
}
