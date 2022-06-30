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
    ICategoryRepository Categories { get; }
    IBaseTypeRepository BaseTypes { get; }
    IUsagesRepository Usages { get; }
    ICommissionRepository Commissions { get; }
    IDiscountsRepository Discounts { get; }
    IGendersRepository Genders { get; }
    IFileRepository Files { get; }

    IDiscountCodeRepository DiscountCodes { get; }

    IJobRepository Jobs { get; }
    ICacheRepository Cache { get; }
    IDocumentRepository Document { get; }
    IServantStatus ServantStatus { get; }
    Task<int> Save();
    void BeginTransaction();
    void EndTransaction();
    T? GetException<T>(Exception e) where T : Exception;
}