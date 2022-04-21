using Transportation.Api.Responses;

namespace Tranportation.Api.Responses;

public class TaskResponse
{
    public ulong Id { get; set; }
    public RequestResponse? Request { get; set; }
    public ServantResponse? Servant { get; set; }
    public int Price { get; set; }
    public int Tip { get; set; }
    public sbyte Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}