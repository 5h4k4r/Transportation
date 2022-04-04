using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Settings;

public class DatabaseOptions
{
    public const string Config = "MariaDb";
    [Required]
    public string ConnectionString { get; set; } = string.Empty;
}