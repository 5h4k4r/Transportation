using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infra.Authentication;

public class UserAuthHandler : AuthenticationHandler<UserAuthOptions>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserAuthContext _userAuthContext;

    public UserAuthHandler(
        UrlEncoder encoder,
        UserAuthContext authContext,
        IOptionsMonitor<UserAuthOptions> options,
        ILoggerFactory logger,
        ISystemClock clock,
        IUnitOfWork unitOfWork
    ) : base(options, logger, encoder, clock)
    {
        _userAuthContext = authContext;
        _unitOfWork = unitOfWork;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        // if(isDevelopment)

        var authHeader = Request.Headers[isDevelopment ? "user" : "auth"].FirstOrDefault();

        //  TODO: when the header is empty the returned result is empty
        if (string.IsNullOrEmpty(authHeader))
            return AuthenticateResult.Fail("No Header is found");

        try
        {
            var resp = JsonSerializer.Deserialize<AuthUser>(authHeader, _jsonSerializerOptions);

            if (resp is not { } user)
                // return AuthenticateResult.Fail(new GatewayAuthException(GatewayAuthException.ErrorCode.InvalidUserModel, "User ID is empty"));
                return AuthenticateResult.Fail(new NotImplementedException());

            var databaseUser = await _unitOfWork.User.GetUserByAuthId(resp.Id);
            if (databaseUser is null)
                return AuthenticateResult.Fail("User not found");

            resp.RoleUsers = databaseUser.RoleUsers.Select(x => x.RoleId);
            resp.AreaId = databaseUser.AreaId;
            resp.LanguageId = databaseUser.LanguageId;
            resp.MySqlId = databaseUser.Id;
            _userAuthContext.SetAuthUser(resp);

            var claimsIdentity = new ClaimsIdentity(GenerateClaims(user), nameof(UserAuthHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch (JsonException e)
        {
            return AuthenticateResult.Fail(e);
        }
    }

    private static IEnumerable<Claim> GenerateClaims(in AuthUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.MobilePhone, user.Mobile)
        };

        if (user.RoleUsers is not null)
            claims.Add(new Claim(ClaimTypes.Role, JsonSerializer.Serialize(user.RoleUsers)));

        if (user.AreaId is not null)
            claims.Add(new Claim(ClaimTypes.Country, user.AreaId.ToString()));

        if (user.LanguageId is not null)
            claims.Add(new Claim(ClaimTypes.Locality, user.LanguageId.ToString()));

        if (user.MySqlId is not null)
            claims.Add(new Claim(ClaimTypes.Sid, user.MySqlId.ToString()));

        return claims;
    }
}