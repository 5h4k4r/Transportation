using Transportation.Api.Model;

namespace Tranportation.Api.Repositories;


public interface IEmployeesRepository
{
    Task<Employee?> GetEmployeeByUserId(ulong Id);
}