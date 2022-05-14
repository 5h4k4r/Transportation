using Core.Common;
using Core.Models;

namespace Core.Interfaces;

public interface IVehiclesRepository
{

    Task<List<VehicleDTO>> ListVehicle(PaginatedRequest model);
}