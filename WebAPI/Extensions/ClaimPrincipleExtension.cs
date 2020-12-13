using System.Security.Claims;

namespace WebAPI.Extensions
{
    public static class ClaimPrincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}