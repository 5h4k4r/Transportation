using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tranportation.Api.Requests;
using Transportation.Api.Model;
using Task = System.Threading.Tasks.Task;
namespace Tranportation.Api.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    protected transportationContext _context;
    public EmployeesRepository(transportationContext context)
    {
        _context = context;
    }

    public EntityEntry<Employee> ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model)
    {
        return _context.Employees.Update(new Employee { UserId = model.UserId, LanguageId = (uint)model.LanguageId!.Value });
    }

    public async Task<Employee?> GetEmployeeByUserId(ulong Id) =>
         await _context.Employees.Where(x => x.UserId == Id).FirstOrDefaultAsync();
}