using Transportation.Api.Helpers;
using Transportation.Api.Model;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Interfaces;

public interface IAuthRepository
{
    public Task<AuthInfoResponse?> AuthInfo(User authContext);
    public string PreparePhoneNumber(string model);

}