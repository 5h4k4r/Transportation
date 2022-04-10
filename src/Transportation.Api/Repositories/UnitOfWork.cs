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
    private IAuthRepository _auth;
    public IAuthRepository Auth
    {
        get
        {
            if (_auth == null)
            {
                _auth = new AuthRepository(_repoContext);
            }
            return _auth;
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
