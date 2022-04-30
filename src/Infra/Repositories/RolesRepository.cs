using AutoMapper;
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
    public Task<RoleDTO?> GetRoleById(ulong Id)
    {
        return Task.FromResult(_mapper.Map<RoleDTO?>(_context.Roles.Where(x => x.Id == Id).FirstOrDefaultAsync()));
    }

}