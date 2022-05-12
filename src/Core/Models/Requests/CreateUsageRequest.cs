using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class CreateUsageRequest
{
    [Required]
    public string StaticKey { get; set; } = string.Empty;
}