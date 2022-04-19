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
    private IServantsPerformanceRepository? _ServantPerformance;
    public IServantsPerformanceRepository ServantPerformance
    {
        get
        {
            if (_ServantPerformance == null)
                _ServantPerformance = new ServantPerformanceRepository(_repoContext);

            return _ServantPerformance;
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
    public void Save()
    {
        _repoContext.SaveChanges();
    }

    public void Dispose()
    {
        _repoContext.Dispose();
    }
}
