using Microsoft.EntityFrameworkCore;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class AreaDepartmentsRepository : IAreaDepartmentsRepository
{
    protected transportationContext _context;
    public AreaDepartmentsRepository(transportationContext context)
    {
        _context = context;
    }
    public async Task<List<AreaDepartment>> GetAreaDepartmentsByRoleUserId(ulong Id) =>
         await _context.AreaDepartments.Where(x => x.RoleUserId == Id).ToListAsync();

}