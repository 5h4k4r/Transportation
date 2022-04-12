using Microsoft.EntityFrameworkCore;
using Transportation.Api.Extensions;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Repositories;

public class ServantPerformanceRepository : IServantsPerformanceRepository
{
    protected transportationContext _context;
    public ServantPerformanceRepository(transportationContext context)
    {
        _context = context;
    }

    public Task<Servant?> GetServantById(int Id) => _context.Servants.Where(x => x.Id == Id).FirstOrDefaultAsync();

    public Task<ServantPerformanceResponse?> ServantPerformance(ServantPerformanceRequest model)
    {
        throw new NotImplementedException();
    }
}