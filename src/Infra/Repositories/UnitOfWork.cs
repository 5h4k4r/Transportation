using AutoMapper;
using Core.Interfaces;
using Core.Repositories;
using Infra.Entities;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly transportationContext _repoContext;
    private readonly IMapper _mapper;

    public UnitOfWork(transportationContext repositoryContext, IMapper mapper)
    {
        _repoContext = repositoryContext;
        _mapper = mapper;

    }

    private IServantsRepository? _Servants;
    public IServantsRepository Servants
    {
        get
        {
            if (_Servants == null)
                _Servants = new ServantsRepository(_repoContext, _mapper);

            return _Servants;
        }
    }

    private IUsersRepository? _User;
    public IUsersRepository User
    {
        get
        {
            if (_User == null)
                _User = new UsersRepository(_repoContext, _mapper);

            return _User;
        }
    }

    private IGendersRepository? _Gender;
    public IGendersRepository Genders
    {
        get
        {
            if (_Gender == null)
                _Gender = new GendersRepository(_repoContext, _mapper);

            return _Gender;
        }
    }

    public ITasksRepository? _Tasks;
    public ITasksRepository Tasks
    {
        get
        {
            if (_Tasks == null)
                _Tasks = new TasksRepository(_repoContext, _mapper);

            return _Tasks;
        }
    }
    public IRoleUsersRepository? _RoleUsers;
    public IRoleUsersRepository RoleUsers
    {
        get
        {
            if (_RoleUsers == null)
                _RoleUsers = new RoleUserRepository(_repoContext, _mapper);

            return _RoleUsers;
        }
    }

    public IRolesRepository? _Roles;
    public IRolesRepository Roles
    {
        get
        {
            if (_Roles == null)
                _Roles = new RolesRepository(_repoContext, _mapper);

            return _Roles;
        }
    }

    public IAreaInfosRepository? _AreaInfos;
    public IAreaInfosRepository AreaInfos
    {
        get
        {
            if (_AreaInfos == null)
                _AreaInfos = new AreaInfosRepository(_repoContext, _mapper);

            return _AreaInfos;
        }
    }

    public IAreaDepartmentsRepository? _AreaDepartments;
    public IAreaDepartmentsRepository AreaDepartments
    {
        get
        {
            if (_AreaDepartments == null)
                _AreaDepartments = new AreaDepartmentsRepository(_repoContext, _mapper);

            return _AreaDepartments;
        }
    }

    public IDepartmentsRepository? _Departments;
    public IDepartmentsRepository Departments
    {
        get
        {
            if (_Departments == null)
                _Departments = new DepartmentsRepository(_repoContext, _mapper);

            return _Departments;
        }
    }

    public IEmployeesRepository? _Employees;
    public IEmployeesRepository Employees
    {
        get
        {
            if (_Employees == null)
                _Employees = new EmployeesRepository(_repoContext, _mapper);

            return _Employees;
        }
    }

    public ILanguagesRepository? _Languages;
    public ILanguagesRepository Languages
    {
        get
        {
            if (_Languages == null)
                _Languages = new LanguagesRepository(_repoContext, _mapper);

            return _Languages;
        }
    }

    public IServantWorkDaysRepository? _ServantWorkDays;
    public IServantWorkDaysRepository ServantWorkDays
    {
        get
        {
            if (_ServantWorkDays == null)
                _ServantWorkDays = new ServantWorkDaysRepository(_repoContext, _mapper);

            return _ServantWorkDays;
        }
    }
    public IUsagesRepository? _Usages;
    public IUsagesRepository Usages
    {
        get
        {
            if (_Usages == null)
                _Usages = new UsagesRepository(_repoContext, _mapper);

            return _Usages;
        }
    }



    public Task<int> Save() => _repoContext.SaveChangesAsync();


    public void Dispose()
    {
        _repoContext.Dispose();
    }
    public T? GetException<T>(Exception exception)
    where T : Exception
    {
        Exception innerException = exception;
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
