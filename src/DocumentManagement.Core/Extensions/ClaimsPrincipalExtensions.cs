using System.Security.Claims;

namespace DocumentManagement.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal) =>
            principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "System";
    }
}
