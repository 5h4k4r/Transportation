using Core.Validations;

namespace Core.Models.Requests;

public class ChangeServantStatusRequest
{
    /// <summary>
    ///     Allowed Values: block, online, offline, passive
    /// </summary>
    [AllowedValues("block", "online", "offline", "passive")]
    public string Status { get; set; }
}