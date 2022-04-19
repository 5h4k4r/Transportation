using Microsoft.EntityFrameworkCore;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class RolesRepository : IRolesRepository
{
    protected transportationContext _context;
    public RolesRepository(transportationContext context)
    {
        _context = context;
    }
    public Task<Role?> GetRoleById(ulong Id)
    {
        return _context.Roles.Where(x => x.Id == Id).FirstOrDefaultAsync();
    }

}