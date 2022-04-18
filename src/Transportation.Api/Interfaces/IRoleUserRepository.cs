using Transportation.Api.Model;

namespace Tranportation.Api.Repositories;


public interface IRoleUserRepository
{
    Task<RoleUser?> GetRoleUserByUserId(ulong userId);

}