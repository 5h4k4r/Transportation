using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
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

    public Task<AreaInfoDTO?> GetAreaInfoById(ulong Id) =>
     _context.AreaInfos
             .ProjectTo<AreaInfoDTO>(_mapper.ConfigurationProvider)
             .Where(x => x.Id == Id)
             .FirstOrDefaultAsync();


    public Task<AreaInfoDTO?> GetAreaInfoByUser(UserDTO user) =>
         _context.AreaInfos.Where(x => x.Id == user.AreaId).ProjectTo<AreaInfoDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();


    public Task<AreaInfoDTO?> GetAreaInfoByTitle(string Title) =>
    _context.AreaInfos
            .Where(x => x.Title == Title)
            .ProjectTo<AreaInfoDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();



    public Task<List<AreaInfoDTO>> ListAreaInfos() => _context.AreaInfos.ProjectTo<AreaInfoDTO>(_mapper.ConfigurationProvider).ToListAsync();


}