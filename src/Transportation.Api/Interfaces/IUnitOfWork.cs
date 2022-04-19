using Tranportation.Api.Interfaces;
using Tranportation.Api.Repositories;

namespace Transportation.Api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IUsersRepository User { get; }
    IServantsPerformanceRepository ServantPerformance { get; }
    ITasksRepository Tasks { get; }
    IRoleUsersRepository RoleUsers { get; }
    IAreaInfosRepository AreaInfos { get; }
    IAreaDepartmentsRepository AreaDepartments { get; }
    IDepartmentsRepository Departments { get; }
    void Save();
}
