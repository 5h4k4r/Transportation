using Core.Validations;

namespace Core.Models.Requests;

public class ListServantsWithTheirStatusesRequest
{
    /// <summary>
    ///     Allowed Values : online, offline, passive, in-trip, block
    /// </summary>
    [AllowedValues("online", "offline", "passive", "in-trip", "block")]
    public string Status { get; set; }
}