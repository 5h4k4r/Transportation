using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tranportation.Api.Interfaces;
using Transportation.Api.Extensions;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class UsersRepository : IUsersRepository
{

    private readonly transportationContext _context;

    public UsersRepository(transportationContext context)
    {
        _context = context;

    }

    public Task<User?> GetUserById([Required] int Id) => _context.Users
    .Where(x => x.Id == (ulong)Id)
    .FirstOrDefaultAsync();
    public Task<User?> GetUserByPhone([Required] string Phone, bool withRoleUsers = false) => _context.Users
    .Where(x => x.Mobile == Phone)
    .WithRoleUser(withRoleUsers)
    .FirstOrDefaultAsync();


    public Task<User?> GetUserByAuthId([Required] string AuthId, bool withRoleUsers = false) => _context.Users
    .Where(x => x.AuthId == AuthId)
    .WithRoleUser(withRoleUsers)
    .FirstOrDefaultAsync();



}