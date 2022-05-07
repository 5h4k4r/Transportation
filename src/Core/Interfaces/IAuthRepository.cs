using Core.Auth;
using Core.Helpers;

using Core.Models;

namespace Core.Interfaces;

public interface IAuthRepository
{
    public Task<AuthInfo?> AuthInfo(UserDTO authContext);
    public string PreparePhoneNumber(string model);

}