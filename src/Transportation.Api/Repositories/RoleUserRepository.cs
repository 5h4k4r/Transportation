using Microsoft.EntityFrameworkCore;
using Tranportation.Api.Repositories;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class RoleUserRepository : IRoleUserRepository
{

    private readonly transportationContext _context;

    public RoleUserRepository(transportationContext context)
    {
        _context = context;
    }

    public Task<RoleUser?> GetRoleUserByUserId(ulong userId) => _context.RoleUsers.Where(x => x.UserId == userId).FirstOrDefaultAsync();

}