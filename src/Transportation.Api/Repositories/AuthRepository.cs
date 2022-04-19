using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Transportation.Api.Helpers;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;
using Transportation.Api.Requests;
using Transportation.Api.Responses;


namespace Transportation.Api.Repositories;

public class AuthRepository : IAuthRepository
{

    protected transportationContext _context;
    public AuthRepository(transportationContext context)
    {
        _context = context;
    }

    public async Task<AuthInfoResponse?> AuthInfo(User user)
    {

        var AreaInfo = await _context.AreaInfos.Where(x => x.Id == user.AreaId).FirstOrDefaultAsync();

        RoleUser? RoleUserWithDepartment = new();
        user.RoleUsers.OrderBy(x => x.RoleId).ToList().ForEach(RoleUser =>
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

        AuthInfoResponse authInfoResponse = new()
        {
            BirthDate = user.BirthDate,
            Id = user?.Id,
            AreaId = AreaInfo?.Id,
            AuthId = user.AuthId,
            MapCenter = new MapCenter(AreaInfo?.Center),
            Department = new Responses.Department
            {
                Id = Department.Id,
                Title = Department.Title,
                Role = new Responses.Role
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