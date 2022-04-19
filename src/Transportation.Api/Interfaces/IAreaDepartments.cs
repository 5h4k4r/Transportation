using Transportation.Api.Model;

namespace Transportation.Api.Interfaces
{
    public interface IAreaDepartmentsRepository
    {
        Task<List<AreaDepartment>> GetAreaDepartmentsByRoleUserId(ulong Id);
    }
}