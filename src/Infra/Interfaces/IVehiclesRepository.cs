using Core.Models.Base;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Entities;
using Task = System.Threading.Tasks.Task;

namespace Infra.Interfaces;

public interface IVehiclesRepository
{
    Task<List<VehicleDtoResponse>> ListVehicle(ListVehiclesRequest model);
    Task<int> ListVehicleCount(ListVehiclesRequest model);

    public Task<VehicleDtoResponse?> GetDetailedVehicleById(ulong id);

    public Task<Vehicle?> GetVehicleById(ulong id);

    public Task<Vehicle> AddVehicle(VehicleDto vehicle);

    public Task<VehicleDetail> AddVehicleDetail(VehicleDetailDto vehicleDetail);

    public Task<List<UserDto>> GetVehicleOwners(ulong id);

    public Task<List<UserDto>> GetVehicleUsers(ulong id);
    public Task<Vehicle> UpdateVehicle(Vehicle vehicle);

    public Task AddServantToVehicle(ulong vehicleId, ulong servantId);

    public Task AddUserToVehicle(ulong vehicleId, ulong servantUserId);

    public Task AddOwnerToVehicle(ulong vehicleId, ulong servantUserId);

    public Task DeleteVehicle(ulong id);

    public Task SubscribeToService(ulong vehicleId, ICollection<ulong> serviceIds, ulong? servantId = null);
}