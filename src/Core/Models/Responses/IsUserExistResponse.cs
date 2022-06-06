using Core.Models.Base;

namespace Core.Models.Repositories;

public class IsUserExistResponse
{
    public bool IsUser { get; set; }
    public bool IsServant { get; set; }
    public UserDto? User { get; set; }
}