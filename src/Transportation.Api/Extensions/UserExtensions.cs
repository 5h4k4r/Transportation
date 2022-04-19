using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Tranportation.Api;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Extensions;

public static class UserExtensions
{
    public static bool HasRole(this User user, int roleId)
    {
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }
    public static bool HasRole(this User user, string roleName)
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

    public static IQueryable<User> WithRoleUser(this IQueryable<User> query, bool withRoleUsers = false)
    {

        if (withRoleUsers)
            return query.Include(x => x.RoleUsers);

        return query;
    }
}