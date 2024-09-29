using Infrastructure.Identity;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Identity;

public static class UserManagerExtension
{
    public static string GetDisplayName(
        this UserManager<ApplicationUser> userManager,
        ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        var user = userManager.GetUserAsync(principal).Result;
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return user.DisplayName;
    }
}
