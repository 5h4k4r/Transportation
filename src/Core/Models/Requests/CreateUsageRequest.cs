using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests;

public class CreateUsageRequest
{
    [Required]
    public string StaticKey { get; set; } = string.Empty;
}