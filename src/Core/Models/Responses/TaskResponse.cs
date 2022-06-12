using Core.Models.Base;

namespace Core.Models.Repositories;

public class TaskResponse
{
    public ulong Id { get; set; }
    public RequestDto? Request { get; set; }
    public ServantDto? Servant { get; set; }
    public int Price { get; set; }
    public int Tip { get; set; }
    public sbyte Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}