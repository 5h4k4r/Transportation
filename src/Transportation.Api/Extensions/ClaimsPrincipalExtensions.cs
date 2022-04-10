using System.Security.Claims;

namespace Transportation.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetJti(this ClaimsPrincipal user)
            => user.FindFirstValue("jti");

    public static string GetAuthId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static bool HasId(this ClaimsPrincipal user, in string id)
        => user.FindFirstValue("sub") == id;

}