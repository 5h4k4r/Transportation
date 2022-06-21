using Core.Models.Base;
using Core.Models.Responses;
using Infra.Entities;

namespace Infra.Interfaces;

public interface IServiceRepository
{
    public Task<List<ListServicesResponses>> ListServices();
    public Task<ServiceAreaTypeDto?> GetServiceById(uint id, uint? serviceId = null);

    Task<ServiceAreaType> CreateServiceAreaType(ServiceAreaTypeDto serviceAreaType);
}