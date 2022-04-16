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
            "superadmin" => 2,
            "admin" => 2,
            "servant" => 2,
            _ => 4,
        };
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }
}