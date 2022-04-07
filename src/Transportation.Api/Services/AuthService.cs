using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Model;
using Transportation.Api.Repositories;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Services;

public class AuthService
{

    private AuthRepository AuthRepository { get; init; }


    public AuthService(AuthRepository _AuthRepository)
    {
        AuthRepository = _AuthRepository;
    }

    public Task<AuthInfoResponse?> AuthInfo(UserAuthContext authContext) => AuthRepository.GetAuthInfo(authContext);
    public Task<object?> Login(LoginRequest model) => AuthRepository.Login(model);
}