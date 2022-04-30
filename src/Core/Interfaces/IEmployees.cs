using Core.Interfaces;
using Core.Models;
using Infra.Requests;
using Task = System.Threading.Tasks.Task;


public interface IEmployeesRepository
{
    Task<EmployeeDTO?> GetEmployeeByUserId(ulong Id);
    EmployeeDTO ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model);
}