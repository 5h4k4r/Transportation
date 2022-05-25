using Core.Models.Base;

namespace Core.Interfaces;

public interface IRoleUsersRepository
{
    Task<RoleUserDto?> GetRoleUserByUserId(ulong userId);

}