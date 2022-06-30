using Core.Models.Base;
using Core.Models.Responses;
using Infra.Entities;

namespace Infra.Interfaces;

public interface IServiceRepository
{
    public Task<List<ListServicesResponses>> ListServices(ulong? languageId = 2);
    public Task<ServiceAreaType?> GetServiceById(ulong id);

    Task<ServiceAreaType> CreateServiceAreaType(ServiceAreaTypeDto serviceAreaType);

    Task<ServiceAreaType?> GetServiceAreaTypeById(uint id);

    Task<ServiceAreaType> UpdateServiceAreaType(ServiceAreaType serviceAreaType);
}