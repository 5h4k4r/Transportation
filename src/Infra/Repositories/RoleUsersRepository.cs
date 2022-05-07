using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class RoleUserRepository : IRoleUsersRepository
{

    private readonly transportationContext _context;
    private readonly IMapper _mapper;
    public RoleUserRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoleUserDTO?> GetRoleUserByUserId(ulong userId)
    {
        var databaseModel = await _context.RoleUsers.Where(x => x.UserId == userId).SingleOrDefaultAsync();
        return _mapper.Map<RoleUserDTO?>(databaseModel);
        //  Task.FromResult(_mapper.Map<RoleUserDTO?>(_context.RoleUsers.Where(x => x.UserId == userId).SingleOrDefaultAsync()));

    }

}