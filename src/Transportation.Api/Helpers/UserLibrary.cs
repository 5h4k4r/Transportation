using Transportation.Api.Model;

namespace Transportation.Api.Helpers;

public static class UserLibrary
{
    public static bool HasRole(this User user, int roleId)
    {
        return user.RoleUsers.Select(x => x.RoleId).Contains((byte)roleId);
    }
    public static bool HasRole(this User user, string roleName)
    {
        var loweredRoleName = roleName.ToLower();


        int roleId = 4;

        switch (loweredRoleName)
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