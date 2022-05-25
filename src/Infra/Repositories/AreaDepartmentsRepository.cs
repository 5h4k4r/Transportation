using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class AreaDepartmentsRepository : IAreaDepartmentsRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;
    public AreaDepartmentsRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<List<AreaDepartmentDto>> GetAreaDepartmentsByRoleUserId(ulong id)
    {
        var database = _context.AreaDepartments.Where(x => x.RoleUserId == id).ToListAsync();

        return Task.FromResult(_mapper.Map<List<AreaDepartmentDto>>(database));

    }
    public Task<AreaDepartmentDto?> GetAreaDepartmentByRoleUserId(ulong id) => Task.FromResult(_context.AreaDepartments.Where(x => x.RoleUserId == id).Include(x => x.Department).ProjectTo<AreaDepartmentDto?>(_mapper.ConfigurationProvider).FirstOrDefault());
}