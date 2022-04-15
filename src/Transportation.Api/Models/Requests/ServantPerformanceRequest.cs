using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Requests;
public class ServantPerformanceRequest
{
    [Required]
    public ulong UserId { get; set; }
    public DateTime? EndAt { get; set; } = DateTime.Today;
    public DateTime? StartAt { get; set; } = DateTime.Today;

}
