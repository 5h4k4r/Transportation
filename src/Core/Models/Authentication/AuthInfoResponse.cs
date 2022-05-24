namespace Core.Auth.Models;

public class AuthInfo
{
    public ulong? Id { get; set; }
    public ulong? AreaId { get; set; }
    public string? AuthId { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public Department? Department { get; set; }
    public ulong GenderId { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }
    public ulong LanguageId { get; set; }
    public MapCenter? MapCenter { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}

public class Department
{
    public ulong Id { get; set; }
    public DepartmentRole? Role { get; set; }
    public string Title { get; set; } = string.Empty;

}

public class DepartmentRole
{
    public ulong? Id { get; set; }
    public IEnumerable<Permission> Permissions { get; set; } = Array.Empty<Permission>();
    public string? Title { get; set; } = string.Empty;
}

public class Permission
{
    public ulong Id { get; set; }
    public string Slug { get; set; } = string.Empty;

}

public class MapCenter
{
    public string Lat { get; set; } = string.Empty;
    public string Lng { get; set; } = string.Empty;
    public MapCenter(string? LatLng)
    {
        if (LatLng is null)
            return;

        var coordinates = LatLng.Split(",");

        if (coordinates.Length > 1)
        {

            Lat = coordinates[0];
            Lng = coordinates[0];
        }
    }
}