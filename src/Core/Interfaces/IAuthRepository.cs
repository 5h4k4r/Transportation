using Core.Auth;
using Core.Helpers;

using Core.Models;

public interface IAuthRepository
{
    public Task<AuthInfo?> AuthInfo(UserDTO authContext);
    public string PreparePhoneNumber(string model);

}