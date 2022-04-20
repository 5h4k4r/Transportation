using Microsoft.EntityFrameworkCore;
using Transportation.Api.Model;

namespace Tranportation.Api.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    protected transportationContext _context;
    public EmployeesRepository(transportationContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetEmployeeByUserId(ulong Id) =>
         await _context.Employees.Where(x => x.UserId == Id).FirstOrDefaultAsync();
}