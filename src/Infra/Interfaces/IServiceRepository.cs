using Core.Models.Base;

namespace Infra.Interfaces;

public interface IServiceRepository
{
    public Task<List<ServiceDto>> ListServices();
}