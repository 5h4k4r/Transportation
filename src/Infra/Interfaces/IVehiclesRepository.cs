using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Task = System.Threading.Tasks.Task;

namespace Infra.Interfaces;

public interface IVehiclesRepository
{
    Task<List<VehicleDto>> ListVehicle(ListVehiclesRequest model);
    Task<int> ListVehicleCount(ListVehiclesRequest model);

    public Task<VehicleDto?> GetVehicleById(ulong id);

    public Task<Vehicle> AddVehicle(VehicleDto vehicle);

    public Task<VehicleDetail> AddVehicleDetail(VehicleDetailDto vehicleDetail);

    public Task<List<UserDto>> GetVehicleOwners(ulong id);

    public Task<List<UserDto>> GetVehicleUsers(ulong id);
    public Task UpdateVehicle(VehicleDto vehicle);

    public Task AddServantToVehicle(ulong vehicleId, ulong servantId);

    public Task DeleteVehicle(ulong id);

    public Task SubscribeVehicleToService(ulong vehicleId, ICollection<ulong> serviceIds);
}