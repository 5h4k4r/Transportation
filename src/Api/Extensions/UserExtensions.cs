
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
namespace Api.Extensions;

public static class UserExtensions
{
    public static bool HasRole(this UserDTO user, int roleId)
    {
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }
    public static bool HasRole(this UserDTO user, string roleName)
    {
        var lowercasedRoleName = roleName.ToLower();


        int roleId = 4;

        roleId = lowercasedRoleName switch
        {
            "superadmin" => 1,
            "admin" => 2,
            "servant" => 2,
            _ => 4,
        };
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }

    public static IQueryable<UserDTO> WithRoleUser(this IQueryable<UserDTO> query, bool withRoleUsers = false)
    {

        if (withRoleUsers)
            return query.Include(x => x.RoleUsers);

        return query;
    }
}