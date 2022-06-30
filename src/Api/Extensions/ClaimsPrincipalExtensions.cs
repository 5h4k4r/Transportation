using System.Security.Claims;
using System.Text.Json;
using Core.Models.Authentication;

namespace Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetJti(this ClaimsPrincipal user)
    {
        return user.FindFirstValue("jti");
    }

    public static string GetAuthId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static IEnumerable<byte> GetRoles(this ClaimsPrincipal user)
    {
        return JsonSerializer.Deserialize<IEnumerable<byte>>(user.FindFirstValue(ClaimTypes.Role))!;
    }

    public static bool HasRole(this ClaimsPrincipal user, Role role)
    {
        return user.GetRoles().Any(x => x == (byte)role);
    }

    public static ulong? GetAreaId(this ClaimsPrincipal user)
    {
        return (ulong)int.Parse(user.FindFirstValue(ClaimTypes.Country));
    }

    public static ulong? GetLanguageId(this ClaimsPrincipal user)
    {
        return (ulong)int.Parse(user.FindFirstValue(ClaimTypes.Locality));
    }

    public static ulong? UserId(this ClaimsPrincipal user)
    {
        return (ulong)int.Parse(user.FindFirstValue(ClaimTypes.Sid));
    }

    public static bool HasId(this ClaimsPrincipal user, in string id)
    {
        return user.FindFirstValue("sub") == id;
    }
}