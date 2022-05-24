using System.Security.Claims;
using System.Text.Json;
using Core.Models;

namespace Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetJti(this ClaimsPrincipal user)
            => user.FindFirstValue("jti");

    public static string GetAuthId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier);
    public static IEnumerable<byte> GetRoles(this ClaimsPrincipal user) => JsonSerializer.Deserialize<IEnumerable<byte>>(user.FindFirstValue(ClaimTypes.Role))!;

    public static bool HasId(this ClaimsPrincipal user, in string id)
        => user.FindFirstValue("sub") == id;

}