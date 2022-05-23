using Core.Common;
using Core.Models;
using Core.Requests;

namespace Core.Interfaces;

public interface IVehiclesRepository
{

    Task<List<VehicleDTO>> ListVehicle(ListVehiclesRequest model);
    Task<int> ListVehicleCount(ListVehiclesRequest model);

    public VehicleDTO AddVehicle(CreateVehicleRequest request);

    public Task<List<UserDTO>> GetVehicleOwners(ulong id);

    public Task<List<UserDTO>> GetVehicleUsers(ulong id);

    public VehicleDetailDTO AddVehicleDetail(CreateVehicleRequest request);


}