namespace Transportation.Api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IServantsPerformanceRepository ServantPerformance { get; }
    void Save();
}
