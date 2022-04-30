using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
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



    public async Task<UserDTO?> GetUserById([Required] int Id)
    {
        var model = await _context.Users
        .Where(x => x.Id == (ulong)Id)
        .FirstOrDefaultAsync();
        // var user = _mapper.Map<UserDTO?>(model);
        var user = new UserDTO();
        return user;
    }

    public Task<UserDTO?> GetUserByPhone([Required] string Phone, bool withRoleUsers = false) =>
    Task.FromResult(_mapper.Map<UserDTO?>(
    _context.Users
    .Where(x => x.Mobile == Phone)
    .WithRoleUser(withRoleUsers)
    .FirstOrDefaultAsync()));


    public Task<UserDTO?> GetUserByAuthId([Required] string AuthId, bool withRoleUsers = false)
    {
        // Task.FromResult(
        //     _mapper.Map<UserDTO?>(_context.Users
        // .Where(x => x.AuthId == AuthId)
        // .WithRoleUser(withRoleUsers)
        // .FirstOrDefaultAsync()));

        var databaseModel = _context.Users
        .Where(x => x.AuthId == AuthId)
        .WithRoleUser(withRoleUsers)
        .SingleOrDefaultAsync();

        // var model = _mapper.Map<UserDTO>(databaseModel);
        // var roles = _mapper.Map<RoleUserDTO>(databaseModel.RoleUsers);
        // model.RoleUsers = roles;

        return Task.FromResult(_mapper.Map<UserDTO?>(databaseModel));

    }


}