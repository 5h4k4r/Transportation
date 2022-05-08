using Core.Interfaces;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IUsersRepository User { get; }
    IServantsRepository Servants { get; }
    ITasksRepository Tasks { get; }
    IRoleUsersRepository RoleUsers { get; }
    IAreaInfosRepository AreaInfos { get; }
    IAreaDepartmentsRepository AreaDepartments { get; }
    IDepartmentsRepository Departments { get; }
    IEmployeesRepository Employees { get; }
    ILanguagesRepository Languages { get; }
    IServantWorkDaysRepository ServantWorkDays { get; }
    IRolesRepository Roles { get; }
    Task<int> Save();
    T? GetException<T>(Exception e) where T : Exception;
}
