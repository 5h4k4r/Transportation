using Core.Models.Base;
using Core.Requests;

namespace Core.Interfaces;

public interface IDepartmentsRepository
{

    Task<DepartmentDto?> GetDepartmentById(ulong Id);
    Task<List<DepartmentDto?>> GetDepartments();
    void CreateDepartment(CreatedDepartmentRequest request);

    void DeleteDepartment(uint id);
    void UpdateDepartment(CreatedDepartmentRequest request, uint id);



}