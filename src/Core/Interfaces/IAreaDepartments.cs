using Core.Models.Base;

namespace Core.Interfaces
{
    public interface IAreaDepartmentsRepository
    {
        Task<List<AreaDepartmentDto>> GetAreaDepartmentsByRoleUserId(ulong id);

        Task<AreaDepartmentDto?> GetAreaDepartmentByRoleUserId(ulong id);
    }
}