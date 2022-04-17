using Tranportation.Api.Interfaces;
using Tranportation.Api.Repositories;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly transportationContext _repoContext;
    public UnitOfWork(transportationContext repositoryContext)
    {
        _repoContext = repositoryContext;
    }
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

    private IUserRepository? _User;
    public IUserRepository User
    {
        get
        {
            if (_User == null)
                _User = new UserRepository(_repoContext);

            return _User;
        }
    }

    public ITasksRepository _Tasks;
    public ITasksRepository Tasks
    {
        get
        {
            if (_Tasks == null)
                _Tasks = new TasksRepository(_repoContext);

            return _Tasks;
        }
    }
    public IRoleUserRepository _RoleUsers;
    public IRoleUserRepository RoleUsers
    {
        get
        {
            if (_RoleUsers == null)
                _RoleUsers = new RoleUserRepository(_repoContext);

            return _RoleUsers;
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
