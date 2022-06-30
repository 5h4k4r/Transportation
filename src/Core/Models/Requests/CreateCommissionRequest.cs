namespace Core.Models.Requests;

public class CreateCommissionRequest
{
    public uint ServiceAreaTypeId { get; set; }
    public double Value { get; set; }
    public bool IsWithdrawFromGift { get; set; }
}