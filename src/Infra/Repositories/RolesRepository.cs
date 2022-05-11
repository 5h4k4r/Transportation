using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;

public class RolesRepository : IRolesRepository
{
    protected transportationContext _context;
    private readonly IMapper _mapper;
    public RolesRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<RoleDTO?> GetRoleById(ulong Id) => _context.Roles.Where(x => x.Id == Id).ProjectTo<RoleDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

    public Task<List<RoleDTO>> ListRoleByTtpe(sbyte TypeId) => _context.Roles.Where(x => x.Type == TypeId).ProjectTo<RoleDTO>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();


}