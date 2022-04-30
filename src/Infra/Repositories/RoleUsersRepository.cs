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

    public Task<RoleUserDTO?> GetRoleUserByUserId(ulong userId) => Task.FromResult(_mapper.Map<RoleUserDTO?>(_context.RoleUsers.Where(x => x.UserId == userId).FirstOrDefaultAsync()));

}