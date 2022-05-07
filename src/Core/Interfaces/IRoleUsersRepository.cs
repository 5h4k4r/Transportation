using Core.Interfaces;
using Core.Models;

namespace Core.Interfaces;

public interface IRoleUsersRepository
{
    Task<RoleUserDTO?> GetRoleUserByUserId(ulong userId);

}