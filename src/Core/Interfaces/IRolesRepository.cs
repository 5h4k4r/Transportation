using Core.Models.Base;

namespace Core.Interfaces;

public interface IRolesRepository
{
    Task<RoleDto?> GetRoleById(ulong id);

    Task<List<RoleDto>> ListRoleByTtpe(sbyte typeId);
}