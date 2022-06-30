using AutoMapper;
using Core.Interfaces;
using Infra.Entities;
using Infra.Interfaces;
using ServiceStack.Redis;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IRedisClientsManagerAsync _cacheService;
    private readonly ICurl _curl;
    private readonly IMapper _mapper;
    private readonly TransportationContext _repoContext;
    private IAreaDepartmentsRepository? _areaDepartments;
    private IAreaInfosRepository? _areaInfos;
    private IBaseTypeRepository? _basetypes;
    private ICacheRepository? _cache;

    private ICategoryRepository? _categories;
    private ICommissionRepository? _commissions;
    private IDepartmentsRepository? _departments;
    private IDiscountCodeRepository? _discountCode;
    private IDiscountsRepository? _discounts;
    private IDocumentRepository? _documents;
    private IEmployeesRepository? _employees;
    private IFileRepository? _files;
    private IGendersRepository? _gender;
    private IJobRepository? _jobs;
    private ILanguagesRepository? _languages;
    private IRolesRepository? _roles;
    private IRoleUsersRepository? _roleUsers;
    private IServantsRepository? _servants;
    private IServantWorkDaysRepository? _servantWorkDays;
    private IServiceRepository? _services;
    private ITasksRepository? _tasks;
    private IUsagesRepository? _usages;
    private IUsersRepository? _user;
    private IVehiclesRepository? _vehicles;

    public UnitOfWork(TransportationContext repositoryContext, IMapper mapper, IRedisClientsManagerAsync cacheService,
        ICurl curl)
    {
        _repoContext = repositoryContext;
        _mapper = mapper;
        _cacheService = cacheService;
        _curl = curl;
    }

    public ICacheRepository Cache => _cache ??= new RedisCacheRepository(_repoContext, _mapper, _cacheService);
    public IServantsRepository Servants => _servants ??= new ServantsRepository(_repoContext, _mapper);
    public IServiceRepository Services => _services ??= new ServicesRepository(_repoContext, _mapper);
    public IVehiclesRepository Vehicles => _vehicles ??= new VehiclesRepository(_repoContext, _mapper);
    public IUsersRepository User => _user ??= new UsersRepository(_repoContext, _mapper);
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(_repoContext, _mapper);
    public IBaseTypeRepository BaseTypes => _basetypes ??= new BaseTypeRepository(_repoContext, _mapper);

    public ICommissionRepository Commissions => _commissions ??= new CommissionRepository(_repoContext, _mapper);
    public IDiscountsRepository Discounts => _discounts ??= new DiscountsRepository(_repoContext, _mapper);

    public IGendersRepository Genders => _gender ??= new GendersRepository(_repoContext, _mapper);
    public IFileRepository Files => _files ??= new FileRepository(_repoContext, _mapper);
    public ITasksRepository Tasks => _tasks ??= new TasksRepository(_repoContext, _mapper);
    public IRoleUsersRepository RoleUsers => _roleUsers ??= new RoleUserRepository(_repoContext, _mapper);

    public IDiscountCodeRepository DiscountCodes =>
        _discountCode ??= new DiscountCodesRepository(_repoContext, _mapper);

    public IRolesRepository Roles => _roles ??= new RolesRepository(_repoContext, _mapper);
    public IAreaInfosRepository AreaInfos => _areaInfos ??= new AreaInfosRepository(_repoContext, _mapper);

    public IAreaDepartmentsRepository AreaDepartments =>
        _areaDepartments ??= new AreaDepartmentsRepository(_repoContext, _mapper);

    public IDepartmentsRepository Departments => _departments ??= new DepartmentsRepository(_repoContext, _mapper);
    public IEmployeesRepository Employees => _employees ??= new EmployeesRepository(_repoContext, _mapper);
    public ILanguagesRepository Languages => _languages ??= new LanguagesRepository(_repoContext, _mapper);
    public IJobRepository Jobs => _jobs ??= new JobRepository(_repoContext, _mapper);

    public IServantWorkDaysRepository ServantWorkDays =>
        _servantWorkDays ??= new ServantWorkDaysRepository(_repoContext);

    public IUsagesRepository Usages => _usages ??= new UsagesRepository(_repoContext, _mapper);
    public IDocumentRepository Document => _documents ??= new DocumentRepository(_repoContext, _mapper);

    public void BeginTransaction()
    {
        _repoContext.Database.BeginTransaction();
    }

    public void EndTransaction()
    {
        _repoContext.Database.CommitTransaction();
    }

    public Task<int> Save()
    {
        return _repoContext.SaveChangesAsync();
    }


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
            if (innerException is T result) return result;

            innerException = innerException.InnerException ?? null;
        }

        return null;
    }
}