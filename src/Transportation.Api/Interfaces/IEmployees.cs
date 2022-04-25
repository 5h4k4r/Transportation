using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tranportation.Api.Requests;
using Transportation.Api.Model;

using Task = System.Threading.Tasks.Task;
namespace Tranportation.Api.Repositories;


public interface IEmployeesRepository
{
    Task<Employee?> GetEmployeeByUserId(ulong Id);
    EntityEntry<Employee> ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model);
}