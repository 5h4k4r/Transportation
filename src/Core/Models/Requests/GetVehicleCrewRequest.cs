using System.ComponentModel.DataAnnotations;

namespace Core.Requests;

public class GetVehicleCrewRequest
{
    [Required]
    public VehicleCrew VehicleCrew { get; set; }
}

public enum VehicleCrew
{
    Owner = 0,
    User = 1

}
