using System.Reflection.Metadata;
using Transportation.Api.Model;

namespace Transportation.Api.Interfaces;

public interface IRolesRepository
{
    Task<Role?> GetRoleById(ulong Id);
}