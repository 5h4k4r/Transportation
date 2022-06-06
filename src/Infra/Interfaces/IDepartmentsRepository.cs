using Core.Models.Base;

namespace Infra.Interfaces;

public interface IDepartmentsRepository
{
    Task<DepartmentDto?> GetDepartmentById(ulong id);
}