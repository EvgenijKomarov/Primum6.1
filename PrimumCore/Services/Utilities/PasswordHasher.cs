using System.Security.Cryptography;

namespace PrimumCore.Services.Utilities
{
    public class PasswordHasher
    {
        private const int SaltSize = 128 / 8;      // 128 бит = 16 байт
        private const int KeySize = 256 / 8;       // 256 бит = 32 байта
        private const int Iterations = 100_000;    // Рекомендуется не менее 100 000

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize
            );

            return $"{Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            string[] parts = hashedPassword.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out int iterations) || iterations <= 0)
                return false;

            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] hash = Convert.FromBase64String(parts[2]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
