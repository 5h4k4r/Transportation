using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Infra.Extensions;
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



    public Task<UserDTO?> GetUserById([Required] int Id) =>
        _context.Users
        .Where(x => x.Id == (ulong)Id)
        .ProjectTo<UserDTO?>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

    public Task<UserDTO?> GetUserByPhone([Required] string Phone, bool withRoleUsers = false) =>
         _context.Users
        .Where(x => x.Mobile == Phone)
        .WithRoleUser(withRoleUsers)
        .ProjectTo<UserDTO?>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();


    public Task<UserDTO?> GetUserByAuthId([Required] string AuthId, bool withRoleUsers = false) => _context.Users
        .Where(x => x.AuthId == AuthId)
        .WithRoleUser(withRoleUsers)
        .ProjectTo<UserDTO?>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

}

