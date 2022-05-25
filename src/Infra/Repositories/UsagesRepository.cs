using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class UsagesRepository : IUsagesRepository
{
    protected TransportationContext _Context;
    private readonly IMapper _mapper;
    public UsagesRepository(TransportationContext context, IMapper mapper)
    {
        _Context = context;
        _mapper = mapper;
    }

    public Task<List<UsageDto>> ListUsages() => _Context.Usages.ProjectTo<UsageDto>(_mapper.ConfigurationProvider).ToListAsync();
    public Task<UsageDto?> GetUsageById(ulong id) => _Context.Usages.ProjectTo<UsageDto>(_mapper.ConfigurationProvider).Where(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateUsage(CreateUsageRequest model)
    {
        var databaseModel = new Usage
        {
            StaticKey = model.StaticKey,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _Context.Usages.AddAsync(databaseModel);
    }
}