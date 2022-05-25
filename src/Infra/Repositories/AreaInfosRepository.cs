using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infra.Repositories;

public class AreaInfosRepository : IAreaInfosRepository
{
    protected TransportationContext _Context;
    private readonly IMapper _mapper;
    public AreaInfosRepository(TransportationContext context, IMapper mapper)
    {
        _Context = context;
        _mapper = mapper;
    }

    public Task<AreaInfoDto?> GetAreaInfoById(ulong id) =>
     _Context.AreaInfos
             .ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider)
             .Where(x => x.Id == id)
             .FirstOrDefaultAsync();


    public Task<AreaInfoDto?> GetAreaInfoByUser(UserDto user) =>
         _Context.AreaInfos.Where(x => x.Id == user.AreaId).ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();


    public Task<AreaInfoDto?> GetAreaInfoByTitle(string title) =>
    _Context.AreaInfos
            .Where(x => x.Title == title)
            .ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();



    public Task<List<AreaInfoDto>> ListAreaInfos(ListAreaInfosRequest? model)
    {
        var query = _Context.AreaInfos.AsQueryable();

        if (model?.Type is not null)
            query = query.Where(x => x.Type == model.Type);

        if (model?.Title is not null)
            query = query.Where(x => x.Title.Equals(model.Title));

        return query.ProjectTo<AreaInfoDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}