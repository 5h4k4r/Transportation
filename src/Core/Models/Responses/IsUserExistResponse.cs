using Core.Models.Base;

namespace Core.Models.Responses;

public class IsUserExistResponse
{
    public bool IsUser { get; set; }
    public bool IsServant { get; set; }
    public UserDto? User { get; set; }
}