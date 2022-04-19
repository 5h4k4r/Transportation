using Transportation.Api.Model;

namespace Tranportation.Api.Repositories;


public interface IRoleUsersRepository
{
    Task<RoleUser?> GetRoleUserByUserId(ulong userId);

}