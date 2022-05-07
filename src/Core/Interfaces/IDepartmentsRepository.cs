using Core.Models;

namespace Core.Interfaces;

public interface IDepartmentsRepository
{

    Task<DepartmentDTO?> GetDepartmentById(ulong Id);
}