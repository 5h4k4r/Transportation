using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class RolesRepository : IRolesRepository
{
    protected TransportationContext _Context;
    private readonly IMapper _mapper;
    public RolesRepository(TransportationContext context, IMapper mapper)
    {
        _Context = context;
        _mapper = mapper;
    }
    public Task<RoleDto?> GetRoleById(ulong id) => _Context.Roles.Where(x => x.Id == id).ProjectTo<RoleDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

    public Task<List<RoleDto>> ListRoleByTtpe(sbyte typeId) => _Context.Roles.Where(x => x.Type == typeId).ProjectTo<RoleDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();


}