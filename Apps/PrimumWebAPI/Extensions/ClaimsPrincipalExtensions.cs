using System.Security.Claims;

namespace PrimumWebAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            Claim id = principal.FindFirst(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("Id not found");
            return int.Parse(id.Value);
        }
    }
}
