using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Responses;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class ServicesRepository : IServiceRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public ServicesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<ListServicesResponses>> ListServices()
    {
        var services = _context.Services
            .Include(x => x.ServiceAreaTypes)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.CategoryTranslations)
            .ProjectTo<ListServicesResponses>(_mapper.ConfigurationProvider).ToList();

        return Task.FromResult(services);
    }

    public Task<ServiceAreaTypeDtoResponse?> GetServiceById(uint id, uint? serviceId = null)
    {
        var query = _context.ServiceAreaTypes.AsQueryable().Where(x => x.Id == id).AsNoTracking();

        if (serviceId.HasValue)
            query = query.Where(x => x.ServiceId == serviceId);

        return Task.FromResult(query.ProjectTo<ServiceAreaTypeDtoResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefault());
    }
}