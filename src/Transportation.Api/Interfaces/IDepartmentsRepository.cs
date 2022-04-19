using Transportation.Api.Model;

namespace Transportation.Api.Interfaces;

public interface IDepartmentsRepository
{

    Task<Department?> GetDepartmentById(ulong Id);
}