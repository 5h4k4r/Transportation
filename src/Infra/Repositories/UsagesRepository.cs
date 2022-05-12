using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models;
using Core.Repositories;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class UsagesRepository : IUsagesRepository
{
    protected transportationContext _context;
    private readonly IMapper _mapper;
    public UsagesRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<UsageDTO>> ListUsages() => _context.Usages.ProjectTo<UsageDTO>(_mapper.ConfigurationProvider).ToListAsync();
    public Task<UsageDTO?> GetUsageById(ulong Id) => _context.Usages.ProjectTo<UsageDTO>(_mapper.ConfigurationProvider).Where(x => x.Id == Id).FirstOrDefaultAsync();

    public async Task CreateUsage(CreateUsageRequest model)
    {
        var databaseModel = new Usage
        {
            StaticKey = model.StaticKey,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _context.Usages.AddAsync(databaseModel);
    }
}