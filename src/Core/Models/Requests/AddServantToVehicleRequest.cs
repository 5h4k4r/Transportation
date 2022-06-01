namespace Core.Models.Requests;

public class AddServantToVehicleRequest
{
    public ulong VehicleId { get; set; }
    public ulong UserId { get; set; }
}