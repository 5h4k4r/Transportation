using Core.Models.Base;

namespace Infra.Interfaces;

public interface IUsersRepository
{
    Task<UserDto?> GetUserById(int id);
    Task<UserDto?> GetUserByPhone(string phone, bool withRoleUsers = false);
    Task<UserDto?> GetUserByAuthId(string authId, bool withRoleUsers = false);
}