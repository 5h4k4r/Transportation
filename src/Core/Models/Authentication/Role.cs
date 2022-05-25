
namespace Core.Models.Authentication;

public enum Role
{
    Employee = 6,
    Client = 5,
    Servant = 4,
    Organization = 3,
    Admin = 2,
    SuperAdmin = 1
}

public static class Roles
{
    public const string SuperAdmin = nameof(Role.SuperAdmin);
    public const string Admin = nameof(Role.Admin);
    public const string Organization = nameof(Role.Organization);
    public const string Servant = nameof(Role.Servant);
    public const string Client = nameof(Role.Client);
    public const string Employee = nameof(Role.Employee);
    public const string SuperAdminOrAdmin = $"{SuperAdmin},{Admin}";
    public const string SuperAdminOrServant = $"{SuperAdmin},{Servant}";
    public const string SuperAdminOrClient = $"{SuperAdmin},{Client}";
    public const string NotClient = $"{SuperAdminOrAdmin},{Servant}";

}
