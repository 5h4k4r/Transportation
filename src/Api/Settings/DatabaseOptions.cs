using System.ComponentModel.DataAnnotations;

namespace Api.Settings;

public class DatabaseOptions
{
    public const string Config = "Database";

    [Required]
    public string ConnectionString { get; set; } = string.Empty;

    [Required]
    public string ServerVersion { get; set; } = "10.5.15-mariadb";
}