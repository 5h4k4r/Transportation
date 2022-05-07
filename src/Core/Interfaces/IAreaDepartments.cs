using Core.Models;

namespace Core.Interfaces
{
    public interface IAreaDepartmentsRepository
    {
        Task<List<AreaDepartmentDTO>> GetAreaDepartmentsByRoleUserId(ulong Id);
    }
}