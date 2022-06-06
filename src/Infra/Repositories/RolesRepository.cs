using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public RolesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<RoleDto?> GetRoleById(ulong id)
    {
        return _context.Roles.Where(x => x.Id == id).ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public Task<List<RoleDto>> ListRoleByTtpe(sbyte typeId)
    {
        return _context.Roles.Where(x => x.Type == typeId).ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .AsNoTracking().ToListAsync();
    }
}