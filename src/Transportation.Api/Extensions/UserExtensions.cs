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

        switch (lowercasedRoleName)
        {
            case "superadmin":
                roleId = 2;
                break;

            case "admin":
                roleId = 2;
                break;

            case "servant":
                roleId = 2;
                break;

            default: roleId = 4; break;
        }

        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }
}