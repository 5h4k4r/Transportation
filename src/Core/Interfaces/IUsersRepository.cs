using Core.Models.Base;

namespace Core.Interfaces;

public interface IUsersRepository
{

    Task<UserDto?> GetUserById(int id);
    Task<UserDto?> GetUserByPhone(string phone, bool withRoleUsers = false);
    Task<UserDto?> GetUserByAuthId(string authId, bool withRoleUsers = false);
    Task<List<UserDto?>> GetUsers();
}