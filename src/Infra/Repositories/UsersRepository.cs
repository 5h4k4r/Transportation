using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Infra.Entities;
using Infra.Extensions;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public UsersRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public Task<UserDto?> GetUserById([Required] int id)
    {
        return _context.Users
            .Where(x => x.Id == (ulong)id)
            .ProjectTo<UserDto?>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public Task<UserDto?> GetUserByPhone([Required] string phone, bool withRoleUsers = false)
    {
        return _context.Users
            .Where(x => x.Mobile == phone)
            .WithRoleUser(withRoleUsers)
            .AsNoTracking()
            .ProjectTo<UserDto?>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }


    public Task<UserDto?> GetUserByAuthId([Required] string authId, bool withRoleUsers = false)
    {
        return _context.Users
            .Where(x => x.AuthId == authId)
            .WithRoleUser(withRoleUsers)
            .ProjectTo<UserDto?>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}