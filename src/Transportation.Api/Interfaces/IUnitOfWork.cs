using Tranportation.Api.Interfaces;
using Tranportation.Api.Repositories;

namespace Transportation.Api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IUserRepository User { get; }
    IServantsPerformanceRepository ServantPerformance { get; }
    ITasksRepository Tasks { get; }
    IRoleUserRepository RoleUsers { get; }
    void Save();
}
