using Core.Models.Base;

namespace Api.Extensions;

public static class UserExtensions
{
    public static bool HasRole(this UserDto user, int roleId)
    {
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }
    public static bool HasRole(this UserDto user, string roleName)
    {
        var lowercasedRoleName = roleName.ToLower();


        var roleId = 4;

        roleId = lowercasedRoleName switch
        {
            "superadmin" => 1,
            "admin" => 2,
            "servant" => 2,
            _ => 4,
        };
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }

}