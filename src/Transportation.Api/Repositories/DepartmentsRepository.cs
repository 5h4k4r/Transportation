using Microsoft.EntityFrameworkCore;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    protected transportationContext _context;
    public DepartmentsRepository(transportationContext context)
    {
        _context = context;
    }
    public async Task<Department?> GetDepartmentById(ulong Id)
    {
        return await _context.Departments.Where(x => x.Id == Id).FirstOrDefaultAsync();
    }
}