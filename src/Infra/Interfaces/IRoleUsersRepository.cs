using Core.Models.Base;

namespace Infra.Interfaces;

public interface IRoleUsersRepository
{
    Task<RoleUserDto?> GetRoleUserByUserId(ulong userId);
}