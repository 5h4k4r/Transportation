using Core.Models.Base;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IVehiclesRepository
{
    Task<List<VehicleDto>> ListVehicle(ListVehiclesRequest model);
    Task<int> ListVehicleCount(ListVehiclesRequest model);

    public Task<VehicleDto?> GetVehicleById(ulong id);

    public void AddVehicle(VehicleDto vehicle);

    public void AddVehicleDetail(VehicleDetailDto vehicleDetail);

    public Task<List<UserDto>> GetVehicleOwners(ulong id);

    public Task<List<UserDto>> GetVehicleUsers(ulong id);
    public Task UpdateVehicle(VehicleDto vehicle);
}