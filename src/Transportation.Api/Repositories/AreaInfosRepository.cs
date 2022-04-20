using Microsoft.EntityFrameworkCore;
using Transportation.Api.Helpers;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class AreaInfosRepository : IAreaInfosRepository
{
    protected transportationContext _context;
    public AreaInfosRepository(transportationContext context)
    {
        _context = context;
    }

    public Task<List<AreaInfo>> GetAreaInfoById(ulong Id) => _context.AreaInfos
    .Where(x => x.Id == Id)
    .ToListAsync();
    public Task<List<AreaInfo>> GetAreaInfoByTitle(string Title) => _context.AreaInfos
    .Where(x => x.Title == Title)
    .ToListAsync();

    public Task<List<AreaInfo>> ListAreaInfos() => _context.AreaInfos.ToListAsync();
}