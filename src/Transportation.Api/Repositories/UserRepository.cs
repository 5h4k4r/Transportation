using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tranportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class UserRepository : IUserRepository
{

    private readonly transportationContext _context;

    public UserRepository(transportationContext context)
    {
        _context = context;
    }

    public Task<User?> GetUserById([Required] int Id) => _context.Users.Where(x => x.Id == (ulong)Id).FirstOrDefaultAsync();
    public Task<User?> GetUserByPhone([Required] string Phone) => _context.Users.Where(x => x.Mobile == Phone).FirstOrDefaultAsync();
    public Task<User?> GetUserByAuthId([Required] string AuthId) => _context.Users.Where(x => x.AuthId == AuthId).FirstOrDefaultAsync();
}