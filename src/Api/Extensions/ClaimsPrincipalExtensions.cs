using System.Security.Claims;
using System.Text.Json;
using Infra.Entities;

namespace Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetJti(this ClaimsPrincipal user)
            => user.FindFirstValue("jti");

    public static string GetAuthId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier);
    public static IEnumerable<byte> GetRoles(this ClaimsPrincipal user) => JsonSerializer.Deserialize<IEnumerable<byte>>(user.FindFirstValue(ClaimTypes.Role))!;
    public static ulong? GetAreaId(this ClaimsPrincipal user) => (ulong)int.Parse(user.FindFirstValue(ClaimTypes.Country));
    public static ulong? LanguageId(this ClaimsPrincipal user) => (ulong)int.Parse(user.FindFirstValue(ClaimTypes.Locality));
    public static ulong? UserId(this ClaimsPrincipal user) => (ulong)int.Parse(user.FindFirstValue(ClaimTypes.Sid));
    public static bool HasId(this ClaimsPrincipal user, in string id)
        => user.FindFirstValue("sub") == id;

}
