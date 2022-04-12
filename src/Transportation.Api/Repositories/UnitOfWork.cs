using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private transportationContext _repoContext;
    public UnitOfWork(transportationContext repositoryContext)
    {
        _repoContext = repositoryContext;
    }
    private IAuthRepository _Auth;
    public IAuthRepository Auth
    {
        get
        {
            if (_Auth == null)
            {
                _Auth = new AuthRepository(_repoContext);
            }
            return _Auth;
        }
    }
    private IServantsPerformanceRepository _ServantPerformance;
    public IServantsPerformanceRepository ServantPerformance
    {
        get
        {
            if (_ServantPerformance == null)
            {
                _ServantPerformance = new ServantPerformanceRepository(_repoContext);
            }
            return _ServantPerformance;
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
