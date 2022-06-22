using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class AreaInfosRepository : IAreaInfosRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public AreaInfosRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<AreaInfoDto?> GetAreaInfoById(ulong id)
    {
        return _context.AreaInfos
            .ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task<AreaInfoDto?> GetAreaInfoByAreaId(string id)
    {
        return _context.AreaInfos
            .ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider)
            .Where(x => x.AreaId == id)
            .FirstOrDefaultAsync();
    }


    public Task<AreaInfoDto?> GetAreaInfoByUser(UserDto user)
    {
        return _context.AreaInfos.Where(x => x.Id == user.AreaId).ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }


    public Task<AreaInfoDto?> GetAreaInfoByTitle(string title)
    {
        return _context.AreaInfos
            .Where(x => x.Title == title)
            .ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }


    public Task<List<AreaInfoDto>> ListAreaInfos(ListAreaInfosRequest? model)
    {
        var query = _context.AreaInfos.AsQueryable();

        if (model?.Type is not null)
            query = query.Where(x => x.Type == model.Type);

        if (model?.Title is not null)
            query = query.Where(x => x.Title.Equals(model.Title));

        return query.ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}