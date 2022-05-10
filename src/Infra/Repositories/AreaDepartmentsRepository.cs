using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class AreaDepartmentsRepository : IAreaDepartmentsRepository
{
    protected transportationContext _context;
    private readonly IMapper _mapper;
    public AreaDepartmentsRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<List<AreaDepartmentDTO>> GetAreaDepartmentsByRoleUserId(ulong Id)
    {
        var database = _context.AreaDepartments.Where(x => x.RoleUserId == Id).ToListAsync();

        return Task.FromResult(_mapper.Map<List<AreaDepartmentDTO>>(database));

    }
    public Task<AreaDepartmentDTO?> GetAreaDepartmentByRoleUserId(ulong id) => Task.FromResult(_context.AreaDepartments.Where(x => x.RoleUserId == id).Include(x => x.Department).ProjectTo<AreaDepartmentDTO?>(_mapper.ConfigurationProvider).FirstOrDefault());
}