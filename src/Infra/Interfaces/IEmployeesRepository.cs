using Core.Models.Base;
using Core.Models.Requests;

namespace Infra.Interfaces;

public interface IEmployeesRepository
{
    Task<EmployeeDto?> GetEmployeeByUserId(ulong id);
    EmployeeDto ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model);
}