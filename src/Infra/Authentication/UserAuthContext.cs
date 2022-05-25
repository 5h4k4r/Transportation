namespace Infra.Authentication;


public class UserAuthContext
{
    private AuthUser? _authUser;

    public AuthUser GetAuthUser()
        => _authUser!;

    public void SetAuthUser(AuthUser authUser)
        => _authUser = authUser;
}