using Payroll.PaygridApi.Helpers;
using Transportation.Api.Model;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Interfaces;

public interface IAuthRepository
{
    Task<User?> Check(string mobile);
    public Task<User?> Login(LoginRequest model);
    public Task<AuthInfoResponse?> AuthInfo(UserAuthContext authContext);
    public string PreparePhoneNumber(string model);

}