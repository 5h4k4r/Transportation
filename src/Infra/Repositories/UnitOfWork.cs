using AutoMapper;
using Core.Interfaces;
using Infra.Entities;
using Infra.Interfaces;
using ServiceStack.Redis;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IRedisClientsManagerAsync _cacheService;
    private readonly TransportationContext _repoContext;
    private readonly IMapper _mapper;
    private readonly ICurl _curl;

    public UnitOfWork(TransportationContext repositoryContext, IMapper mapper, IRedisClientsManagerAsync cacheService,
        ICurl curl)
    {
        _repoContext = repositoryContext;
        _mapper = mapper;
        _cacheService = cacheService;
        _curl = curl;
    }

    private ICacheRepository? _Cache;
    public ICacheRepository Cache => _Cache ??= new RedisCacheRepository(_repoContext, _mapper, _cacheService);
    private IServantsRepository? _servants;
    public IServantsRepository Servants => _servants ??= new ServantsRepository(_repoContext, _mapper);
    private IServiceRepository? _services;
    public IServiceRepository Services => _services ??= new ServicesRepository(_repoContext, _mapper);

    private IVehiclesRepository? _vehicles;
    public IVehiclesRepository Vehicles => _vehicles ??= new VehiclesRepository(_repoContext, _mapper);

    private IUsersRepository? _user;
    public IUsersRepository User => _user ??= new UsersRepository(_repoContext, _mapper);

    private IGendersRepository? _gender;

    public IGendersRepository Genders => _gender ??= new GendersRepository(_repoContext, _mapper);

    private ITasksRepository? _tasks;
    public ITasksRepository Tasks => _tasks ??= new TasksRepository(_repoContext, _mapper);
    private IRoleUsersRepository? _roleUsers;
    public IRoleUsersRepository RoleUsers => _roleUsers ??= new RoleUserRepository(_repoContext, _mapper);

    private IRolesRepository? _roles;
    public IRolesRepository Roles => _roles ??= new RolesRepository(_repoContext, _mapper);

    private IAreaInfosRepository? _areaInfos;
    public IAreaInfosRepository AreaInfos => _areaInfos ??= new AreaInfosRepository(_repoContext, _mapper);

    private IAreaDepartmentsRepository? _areaDepartments;

    public IAreaDepartmentsRepository AreaDepartments =>
        _areaDepartments ??= new AreaDepartmentsRepository(_repoContext, _mapper);

    private IDepartmentsRepository? _departments;
    public IDepartmentsRepository Departments => _departments ??= new DepartmentsRepository(_repoContext, _mapper);

    private IEmployeesRepository? _employees;
    public IEmployeesRepository Employees => _employees ??= new EmployeesRepository(_repoContext, _mapper);

    private ILanguagesRepository? _languages;
    public ILanguagesRepository Languages => _languages ??= new LanguagesRepository(_repoContext, _mapper);
    private IJobRepository? _Jobs;
    public IJobRepository Jobs => _Jobs ??= new JobRepository(_repoContext, _mapper);

    private IServantWorkDaysRepository? _servantWorkDays;

    public IServantWorkDaysRepository ServantWorkDays =>
        _servantWorkDays ??= new ServantWorkDaysRepository(_repoContext);

    private IUsagesRepository? _usages;
    public IUsagesRepository Usages => _usages ??= new UsagesRepository(_repoContext, _mapper);


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