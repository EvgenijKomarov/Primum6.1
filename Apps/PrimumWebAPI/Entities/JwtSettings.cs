using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrimumWebAPI.Entities
{
    public class JwtSettings
    {
        public string Issuer = Environment.GetEnvironmentVariable("WEBAPI_JWT_ISSUER") ?? "https://primum-school.com";
        public string Audience = Environment.GetEnvironmentVariable("WEBAPI_JWT_AUDIENCE") ?? "https://primum-school.com";
        public SymmetricSecurityKey Seed = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("WEBAPI_JWT_SEED") ?? "your-super-secret-key-that-should-be-at-least-32-characters-long"));
        public int ExpirationMinutes = int.Parse(Environment.GetEnvironmentVariable("WEBAPI_JWT_LIFETIME_MINUTES") ?? "60");
    }
}
