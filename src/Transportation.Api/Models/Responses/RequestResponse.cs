namespace Transportation.Api.Responses;

public class RequestResponse
{
    public ulong Id { get; set; }
    public ulong ServiceAreaTypeId { get; set; }
    public DateTime? ReserveTime { get; set; }
    public int KubakPrice { get; set; }
    public int UserPrice { get; set; }
    public string? Discount { get; set; }
    public string? Type { get; set; }
    public sbyte Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public RequestResponse(Model.Request model)
    {

        Id = model.Id;
        CreatedAt = model.CreatedAt;
        UpdatedAt = model.UpdatedAt;
        Discount = model.Discount;
        KubakPrice = model.KubakPrice;
        ReserveTime = model.ReserveTime;
        ServiceAreaTypeId = model.ServiceAreaTypeId;
        Status = model.Status;
        Type = model.Type;
        UserPrice = model.UserPrice;
    }
}