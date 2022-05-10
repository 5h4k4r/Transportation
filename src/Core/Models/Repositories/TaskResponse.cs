using Core.Models;

namespace Core.Repositories;

public class TaskResponse
{
    public ulong Id { get; set; }
    public RequestDTO? Request { get; set; }
    public ServantDTO? Servant { get; set; }
    public int Price { get; set; }
    public int Tip { get; set; }
    public sbyte Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}