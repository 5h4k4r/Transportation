using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infra.Repositories;
public class UsersRepository : IUsersRepository
{


    private readonly transportationContext _context;
    private readonly IMapper _mapper;
    public UsersRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public Task<UserDTO?> GetUserByAuthId(string AuthId, bool withRoleUsers = false)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO?> GetUserById([Required] int Id)
    {
        var model = await _context.Users
        .Where(x => x.Id == (ulong)Id)
        .FirstOrDefaultAsync();
        var user = _mapper.Map<UserDTO?>(model);
        return user;
    }

    public Task<UserDTO?> GetUserByPhone(string Phone, bool withRoleUsers = false)
    {
        throw new NotImplementedException();
    }


}