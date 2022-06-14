using Core.Models.Responses;

namespace Infra.Interfaces;

public interface IServiceRepository
{
    public Task<List<ListServicesResponses>> ListServices();
    public Task<ServiceAreaTypeDtoResponse?> GetServiceById(uint id, uint? serviceId = null);
}