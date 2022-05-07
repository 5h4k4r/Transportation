using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Auth;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infra.Repositories;

public class AuthRepository : IAuthRepository
{

    protected transportationContext _context;
    private readonly IMapper _mapper;
    public AuthRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuthInfo?> AuthInfo(UserDTO user)
    {
        var databaseUser = _mapper.Map<User>(user);

        var AreaInfo = await _context.AreaInfos.Where(x => x.Id == databaseUser.AreaId).FirstOrDefaultAsync();

        RoleUser? RoleUserWithDepartment = new();
        databaseUser.RoleUsers.OrderBy(x => x.RoleId).ToList().ForEach(RoleUser =>
        {

            var AreaDepartment = _context.AreaDepartments.Where(x => x.RoleUserId == RoleUser.Id).Include(x => x.Department).FirstOrDefault();

            if (AreaDepartment?.Department is not null)
                RoleUserWithDepartment = RoleUser;
        });
        if (RoleUserWithDepartment is null)
            return null;

        var AreaDepartments = await _context.AreaDepartments.Where(x => x.RoleUserId == RoleUserWithDepartment.Id).ToListAsync();
        var Department = await _context.Departments.Where(x => x.Id == 1).FirstOrDefaultAsync();

        if (Department is null)
            return null;

        Department.AreaDepartments = AreaDepartments;

        var CurrentRole = await _context.Roles.Where(x => x.Id == RoleUserWithDepartment.RoleId).FirstOrDefaultAsync();

        AuthInfo authInfoResponse = new()
        {
            BirthDate = user.BirthDate,
            Id = user?.Id,
            AreaId = AreaInfo?.Id,
            AuthId = user?.AuthId,
            MapCenter = new MapCenter(AreaInfo?.Center),
            Department = new Core.Auth.Department
            {
                Id = Department.Id,
                Title = Department.Title,
                Role = new Core.Auth.Role
                {
                    Id = CurrentRole?.Id,
                    Title = CurrentRole?.Title,
                }
            },
            Mobile = user!.Mobile,
            Name = user?.Name,
            Version = "V3",
            IsAdmin = CurrentRole?.Id == 2 || CurrentRole?.Id == 6,
            IsSuperAdmin = CurrentRole?.Id == 1
        };

        return authInfoResponse;
    }


    public string PreparePhoneNumber([Required] string model)
    {
        if (model[0] != '+')
            model = "+" + model;


        return model;
    }



}