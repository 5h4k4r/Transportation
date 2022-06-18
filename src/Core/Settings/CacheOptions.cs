using System.ComponentModel.DataAnnotations;

namespace Core.Settings;

public class CacheOptions
{
    public const string Config = "Cache";

    [Required] public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    ///     a Key that is prepended with all the cache keys used by this application
    /// </summary>
    [Required]
    public string InstanceName { get; set; } = "TPDotnet_";
}