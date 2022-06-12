using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
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

    public Task<List<ServiceDto>> ListServices()
    {
        var services = _context.Services
            .Include(x => x.ServiceAreaTypes)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.CategoryTranslations.Where(s => s.LanguageId == 2))
            .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider).ToList();

        return Task.FromResult(services);
    }
}