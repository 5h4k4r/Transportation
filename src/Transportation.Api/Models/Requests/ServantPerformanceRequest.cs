using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Requests;
public class ServantPerformanceRequest
{
    public ulong UserId { get; set; }
    public string EndAt { get; set; } = string.Empty;
    public string StartAt { get; set; } = string.Empty;

}
