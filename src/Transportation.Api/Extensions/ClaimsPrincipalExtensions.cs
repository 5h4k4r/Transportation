using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Transportation.Api.Model;

namespace BabyCareApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    private static transportationContext context = new transportationContext();
    public static IEnumerable<Role> GetRole(this ClaimsPrincipal user)
    {
        var roleId = user.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrEmpty(roleId))
            return null;

        return context.Roles.Where(role => role.Id == Int32.Parse(roleId));


    }

    public static string GetJti(this ClaimsPrincipal user)
            => user.FindFirstValue("jti");

    public static string GetAuthId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static bool HasId(this ClaimsPrincipal user, in string id)
        => user.FindFirstValue("sub") == id;

}