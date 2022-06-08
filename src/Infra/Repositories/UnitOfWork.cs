using AutoMapper;
using Core.Interfaces;
using Infra.Entities;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TransportationContext _repoContext;
    private readonly IMapper _mapper;

    public UnitOfWork(TransportationContext repositoryContext, IMapper mapper)
    {
        _repoContext = repositoryContext;
        _mapper = mapper;

    }

    private IServantsRepository? _Servants;
    public IServantsRepository Servants => _Servants ??= new ServantsRepository(_repoContext, _mapper);

    private IVehiclesRepository? _Vehicles;
    public IVehiclesRepository Vehicles => _Vehicles ??= new VehiclesRepository(_repoContext, _mapper);

    private IUsersRepository? _User;
    public IUsersRepository User => _User ??= new UsersRepository(_repoContext, _mapper);

    private IGendersRepository? _Gender;

    public IGendersRepository Genders => _Gender ??= new GendersRepository(_repoContext, _mapper);

    private ITasksRepository? _Tasks;
    public ITasksRepository Tasks => _Tasks ??= new TasksRepository(_repoContext, _mapper);
    private IRoleUsersRepository? _RoleUsers;

    public IRoleUsersRepository RoleUsers => _RoleUsers ??= new RoleUserRepository(_repoContext, _mapper);
    private IDiscountCodeRepository? _DiscountCode;

    public IDiscountCodeRepository DiscountCode => _DiscountCode ?? new DiscountCodesRepository(_repoContext, _mapper);

    private IRolesRepository? _Roles;
    public IRolesRepository Roles => _Roles ??= new RolesRepository(_repoContext, _mapper);

    private IAreaInfosRepository? _AreaInfos;
    public IAreaInfosRepository AreaInfos => _AreaInfos ??= new AreaInfosRepository(_repoContext, _mapper);

    private IAreaDepartmentsRepository? _AreaDepartments;

    public IAreaDepartmentsRepository AreaDepartments =>
        _AreaDepartments ??= new AreaDepartmentsRepository(_repoContext, _mapper);

    private IDepartmentsRepository? _Departments;
    public IDepartmentsRepository Departments => _Departments ??= new DepartmentsRepository(_repoContext, _mapper);

    private IEmployeesRepository? _Employees;
    public IEmployeesRepository Employees => _Employees ??= new EmployeesRepository(_repoContext, _mapper);

    private ILanguagesRepository? _Languages;
    public ILanguagesRepository Languages => _Languages ??= new LanguagesRepository(_repoContext, _mapper);

    private IServantWorkDaysRepository? _ServantWorkDays;

    public IServantWorkDaysRepository ServantWorkDays =>
        _ServantWorkDays ??= new ServantWorkDaysRepository(_repoContext);

    private IUsagesRepository? _Usages;
    public IUsagesRepository Usages => _Usages ??= new UsagesRepository(_repoContext, _mapper);


    public Task<int> Save() => _repoContext.SaveChangesAsync();


    public void Dispose()
    {
        _repoContext.Dispose();
    }

    public T? GetException<T>(Exception exception)
        where T : Exception
    {
        var innerException = exception;
        while (innerException != null)
        {
            if (innerException is T result)
            {
                return result;
            }

            innerException = innerException.InnerException ?? null;
        }

        return null;
    }
}