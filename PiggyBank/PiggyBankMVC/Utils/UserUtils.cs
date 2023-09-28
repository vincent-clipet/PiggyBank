using System.Security.Claims;

namespace PiggyBankMVC.Utils
{
    public static class UserUtils
    {
        public static string? GetUserId(ClaimsPrincipal u)
        {
            return u?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
