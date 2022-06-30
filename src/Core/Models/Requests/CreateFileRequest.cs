using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests;

public class CreateFileRequest
{
    [Required] [MinLength(1)] public List<string> Paths { get; set; }
}