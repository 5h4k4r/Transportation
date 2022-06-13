using Core.Interfaces;

namespace Infra.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUsersRepository User { get; }
    IServantsRepository Servants { get; }
    IVehiclesRepository Vehicles { get; }
    ITasksRepository Tasks { get; }
    IRoleUsersRepository RoleUsers { get; }
    IAreaInfosRepository AreaInfos { get; }
    IAreaDepartmentsRepository AreaDepartments { get; }
    IDepartmentsRepository Departments { get; }
    IEmployeesRepository Employees { get; }
    ILanguagesRepository Languages { get; }
    IServantWorkDaysRepository ServantWorkDays { get; }
    IRolesRepository Roles { get; }
    IServiceRepository Services { get; }
    IUsagesRepository Usages { get; }

    IGendersRepository Genders { get; }
    
    IDiscountCodeRepository DiscountCodes { get; }
    
    IJobRepository Jobs { get; }
    ICacheRepository Cache { get; }
    Task<int> Save();
    T? GetException<T>(Exception e) where T : Exception;
}
