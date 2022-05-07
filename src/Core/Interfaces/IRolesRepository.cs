using System.Reflection.Metadata;
using Core.Models;

namespace Core.Interfaces;

public interface IRolesRepository
{
    Task<RoleDTO?> GetRoleById(ulong Id);
}