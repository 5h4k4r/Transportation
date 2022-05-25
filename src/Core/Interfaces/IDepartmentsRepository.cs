using Core.Models.Base;

namespace Core.Interfaces;

public interface IDepartmentsRepository
{

    Task<DepartmentDto?> GetDepartmentById(ulong id);

}