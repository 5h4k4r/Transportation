using Tranportation.Api.Interfaces;
using Tranportation.Api.Repositories;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly transportationContext _repoContext;
    public UnitOfWork(transportationContext repositoryContext) => _repoContext = repositoryContext;
    private IAuthRepository? _Auth;
    public IAuthRepository Auth
    {
        get
        {
            if (_Auth == null)
                _Auth = new AuthRepository(_repoContext);

            return _Auth;
        }
    }
    private IServantsRepository? _Servants;
    public IServantsRepository Servants
    {
        get
        {
            if (_Servants == null)
                _Servants = new ServantsRepository(_repoContext);

            return _Servants;
        }
    }

    private IUsersRepository? _User;
    public IUsersRepository User
    {
        get
        {
            if (_User == null)
                _User = new UsersRepository(_repoContext);

            return _User;
        }
    }

    public ITasksRepository? _Tasks;
    public ITasksRepository Tasks
    {
        get
        {
            if (_Tasks == null)
                _Tasks = new TasksRepository(_repoContext);

            return _Tasks;
        }
    }
    public IRoleUsersRepository? _RoleUsers;
    public IRoleUsersRepository RoleUsers
    {
        get
        {
            if (_RoleUsers == null)
                _RoleUsers = new RoleUserRepository(_repoContext);

            return _RoleUsers;
        }
    }

    public IAreaInfosRepository? _AreaInfos;
    public IAreaInfosRepository AreaInfos
    {
        get
        {
            if (_AreaInfos == null)
                _AreaInfos = new AreaInfosRepository(_repoContext);

            return _AreaInfos;
        }
    }

    public IAreaDepartmentsRepository? _AreaDepartments;
    public IAreaDepartmentsRepository AreaDepartments
    {
        get
        {
            if (_AreaDepartments == null)
                _AreaDepartments = new AreaDepartmentsRepository(_repoContext);

            return _AreaDepartments;
        }
    }

    public IDepartmentsRepository? _Departments;
    public IDepartmentsRepository Departments
    {
        get
        {
            if (_Departments == null)
                _Departments = new DepartmentsRepository(_repoContext);

            return _Departments;
        }
    }

    public IEmployeesRepository? _Employees;
    public IEmployeesRepository Employees
    {
        get
        {
            if (_Employees == null)
                _Employees = new EmployeesRepository(_repoContext);

            return _Employees;
        }
    }

    public ILanguagesRepository? _Languages;
    public ILanguagesRepository Languages
    {
        get
        {
            if (_Languages == null)
                _Languages = new LanguagesRepository(_repoContext);

            return _Languages;
        }
    }

    public IServantWorkDaysRepository? _ServantWorkDays;
    public IServantWorkDaysRepository ServantWorkDays
    {
        get
        {
            if (_ServantWorkDays == null)
                _ServantWorkDays = new ServantWorkDaysRepository(_repoContext);

            return _ServantWorkDays;
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
