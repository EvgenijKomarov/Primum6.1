using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrimumWebAPI.Entities
{
    public class JwtSettings
    {
        // Значение из .env.example: с ним любой может подписать токен на любого пользователя
        private const string ExampleSeed = "your-super-secret-key-that-should-be-at-least-32-characters-long";
        private const int MinSeedBytes = 32;

        public string Issuer = Environment.GetEnvironmentVariable("WEBAPI_JWT_ISSUER") ?? "https://primum-school.com";
        public string Audience = Environment.GetEnvironmentVariable("WEBAPI_JWT_AUDIENCE") ?? "https://primum-school.com";
        public SymmetricSecurityKey Seed = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ReadSeed()));
        public int ExpirationMinutes = int.Parse(Environment.GetEnvironmentVariable("WEBAPI_JWT_LIFETIME_MINUTES") ?? "60");

        // Без секрета не стартуем: запасной ключ в коде публичен, и токены с ним можно подделать
        private static string ReadSeed()
        {
            var seed = Environment.GetEnvironmentVariable("WEBAPI_JWT_SEED");
            if (string.IsNullOrWhiteSpace(seed))
            { throw new InvalidOperationException("WEBAPI_JWT_SEED is not set. Generate one: openssl rand -base64 48"); }
            if (seed == ExampleSeed)
            { throw new InvalidOperationException("WEBAPI_JWT_SEED uses the example value from .env.example. Generate a new one: openssl rand -base64 48"); }
            if (Encoding.UTF8.GetByteCount(seed) < MinSeedBytes)
            { throw new InvalidOperationException($"WEBAPI_JWT_SEED must be at least {MinSeedBytes} bytes long"); }
            return seed;
        }
    }
}
