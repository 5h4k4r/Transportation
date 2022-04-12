using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Settings;

public class DatabaseOptions
{
    public const string Config = "Database";
    [Required]
    public string ConnectionString { get; set; } = string.Empty;
}