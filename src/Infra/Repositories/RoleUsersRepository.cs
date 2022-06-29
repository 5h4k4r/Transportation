using AutoMapper;
using Core.Models.Base;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class RoleUserRepository : IRoleUsersRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public RoleUserRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoleUserDto?> GetRoleUserByUserId(ulong userId)
    {
        var databaseModel = await _context.RoleUsers.Where(x => x.UserId == userId).SingleOrDefaultAsync();
        return _mapper.Map<RoleUserDto?>(databaseModel);
        //  Task.FromResult(_mapper.Map<RoleUserDTO?>(_context.RoleUsers.Where(x => x.UserId == userId).SingleOrDefaultAsync()));
    }

    public async Task<RoleUser> AddRoleUser(RoleUserDto roleUser)
    {
        var mappedModel = _mapper.Map<RoleUser>(roleUser);
        mappedModel.CreatedAt = DateTime.UtcNow;
        mappedModel.UpdatedAt = DateTime.UtcNow;

        await _context.RoleUsers.AddAsync(mappedModel);
        return mappedModel;
    }
}