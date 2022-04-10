using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Requests;

public class AuthCheckRequest
{

    [Required]
    public string Mobile { get; set; } = string.Empty;

}