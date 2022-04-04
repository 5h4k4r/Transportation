namespace Transportation.Api.Responses;

public class AuthInfoResponse
{
    public Info? Infos { get; set; }
}

public class Info
{
    public ulong? Id { get; set; }
    public ulong? AreaId { get; set; }
    public string? AuthId { get; set; } = string.Empty;
    public string AvatarId { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
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
    public Role? Role { get; set; }
    public string Title { get; set; } = string.Empty;

}

public class Role
{
    public ulong Id { get; set; }
    public IEnumerable<Permission> Permissions { get; set; } = Array.Empty<Permission>();
    public string Title { get; set; } = string.Empty;
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
}