using System.ComponentModel.DataAnnotations;

namespace Core.Requests;

public class ListAreaInfosRequest
{
    /// <summary>
    /// Allowed Values : city
    /// </summary>
    [AllowedValues("city")]
    public string? Type { get; set; }
    public string? Title { get; set; }
}