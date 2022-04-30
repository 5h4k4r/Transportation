using AutoMapper;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;

public class AreaInfosRepository : IAreaInfosRepository
{
    protected transportationContext _context;
    private readonly IMapper _mapper;
    public AreaInfosRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<AreaInfoDTO>> GetAreaInfoById(ulong Id)
    {
        var databaseAreaInfo = _context.AreaInfos
                                       .Where(x => x.Id == Id)
                                       .ToListAsync();

        var areaInfo = Task.FromResult(_mapper.Map<List<AreaInfoDTO>>(databaseAreaInfo));
        return areaInfo;
    }
    public Task<List<AreaInfoDTO>> GetAreaInfoByTitle(string Title)
    {
        var databaseAreaInfo = _context.AreaInfos
                                        .Where(x => x.Title == Title)
                                        .ToListAsync();


        var areaInfo = Task.FromResult(_mapper.Map<List<AreaInfoDTO>>(databaseAreaInfo));
        return areaInfo;

    }

    public Task<List<AreaInfoDTO>> ListAreaInfos()
    {
        var databaseAreaInfo = _context.AreaInfos.ToListAsync();

        var areaInfo = Task.FromResult(_mapper.Map<List<AreaInfoDTO>>(databaseAreaInfo));
        return areaInfo;

    }
}