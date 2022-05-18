using Core.Common;
using Core.Models;
using Core.Requests;

namespace Core.Interfaces;

public interface IVehiclesRepository
{

    Task<List<VehicleDTO>> ListVehicle(ListVehiclesRequest model);
    Task<int> ListVehicleCount(ListVehiclesRequest model);
}