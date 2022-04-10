namespace Transportation.Api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    void Save();
}
