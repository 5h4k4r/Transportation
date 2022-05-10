using Microsoft.AspNetCore.Authentication;
using Transportation.Api.Auth;

namespace Core.Helpers;


public class UserAuthContext
{
    private AuthUser? _authUser;

    public AuthUser GetAuthUser()
        => _authUser!;

    public void SetAuthUser(AuthUser authUser)
        => _authUser = authUser;
}