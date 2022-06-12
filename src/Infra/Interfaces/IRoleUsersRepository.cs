using Core.Models.Base;
using Infra.Entities;

namespace Infra.Interfaces;

public interface IRoleUsersRepository
{
    Task<RoleUserDto?> GetRoleUserByUserId(ulong userId);
    Task<RoleUser> AddRoleUser(RoleUserDto roleUser);
}