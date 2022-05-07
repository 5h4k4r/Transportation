using Core.Interfaces;
using Core.Models;
using Infra.Requests;
using Task = System.Threading.Tasks.Task;
namespace Core.Interfaces;

public interface IEmployeesRepository
{
    Task<EmployeeDTO?> GetEmployeeByUserId(ulong Id);
    EmployeeDTO ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model);
}