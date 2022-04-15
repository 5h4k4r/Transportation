using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Transportation.Api.Auth;

namespace Transportation.Api.Helpers;

public class GatewayAuthHandler : AuthenticationHandler<UserAuthOptions>
{
    private readonly IOptionsMonitor<UserAuthOptions> _Options;
    private readonly JsonSerializerOptions _JsonSerializerOptions;
    private readonly UserAuthContext _UserAuthContext;

    public GatewayAuthHandler(
        UrlEncoder encoder,
        UserAuthContext authContext,
        IOptionsMonitor<UserAuthOptions> options,
        ILoggerFactory logger,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
        _Options = options;
        _UserAuthContext = authContext;

        _JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var auth = Request.Headers["user"].FirstOrDefault();

        //  TODO: when the header is empty the returned result is empty
        if (string.IsNullOrEmpty(auth))
            return Task.FromResult(AuthenticateResult.Fail("No Header is found"));

        try
        {
            var resp = JsonSerializer.Deserialize<AuthUser>(auth!, _JsonSerializerOptions);

            if (resp is not AuthUser user)
                // return AuthenticateResult.Fail(new GatewayAuthException(GatewayAuthException.ErrorCode.InvalidUserModel, "User ID is empty"));
                return Task.FromResult(AuthenticateResult.Fail(new NotImplementedException()));

            //     await _UserService.UpsertAsync(user);

            //     // TODO: Add fake accounting unit
            //     resp.Data.Attributes.AccountingUnit = new AccountingUnit { Id = "YsrbjgBLha1aUYV4svFR8" };

            _UserAuthContext.SetAuthUser(resp);

            var claimsIdentity = new ClaimsIdentity(GenerateClaims(user), nameof(GatewayAuthHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (JsonException e)
        {
            //     var authException = new GatewayAuthException(GatewayAuthException.ErrorCode.InvalidUserModel, e.Message);
            // return AuthenticateResult.Fail(authException);
            return Task.FromResult(AuthenticateResult.Fail(e));
        }

    }

    // protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    // {
    //     var result = await HandleAuthenticateOnceSafeAsync();
    //     if (result.Failure is not GatewayAuthException e)
    //         return;

    //     Response.StatusCode = StatusCodes.Status403Forbidden;
    //     await Response.WriteAsJsonAsync(e.ToJsonApiError().ToJsonApiErrorResponse(), _JsonSerializerOptions);
    // }

    // private bool HasValidSignature(in string user, in string signature)
    //     => _Options.CurrentValue.RsaProvider.VerifyData(Encoding.UTF8.GetBytes(user),
    //                                                     "SHA256",
    //                                                     Convert.FromBase64String(signature));


    private static List<Claim> GenerateClaims(in AuthUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.Mobile)
        };


        return claims;
    }
}