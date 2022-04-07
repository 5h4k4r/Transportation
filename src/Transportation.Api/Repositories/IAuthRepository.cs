using Microsoft.AspNetCore.Mvc;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Repositories;

public interface IAuthRepository
{
    Task<AuthCheckResponse?> Check();
    Task<AuthInfoResponse?> GetAuthInfo(UserAuthContext authContext);
    Task<object?> Login(LoginRequest model);
    string PreparePhoneNumber(string model);

}