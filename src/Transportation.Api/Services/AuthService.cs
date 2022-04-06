using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Model;
using Transportation.Api.Responses;

namespace Transportation.Api.Services;

public class AuthService
{

    private transportationContext context { get; init; }


    public AuthService(transportationContext _context)
    {
        context = _context;
    }

    public async Task<AuthInfoResponse?> AuthInfo(UserAuthContext authContext)
    {
        var authUser = authContext.GetAuthUser();
        var MySqlUser = await context.Users
                                    .Where(x => x.AuthId == authUser.Id)
                                    .FirstOrDefaultAsync();

        if (MySqlUser is null)
            return null;

        var AreaInfo = await context.AreaInfos.Where(x => x.Id == MySqlUser.AreaId).FirstOrDefaultAsync();
        var Employee = await context.Employees.Where(x => x.UserId == MySqlUser.Id).FirstOrDefaultAsync();
        var RoleUsers = await context.RoleUsers.Where(x => x.UserId == MySqlUser.Id).ToListAsync();

        RoleUser RoleUserWithDepartment = null;
        RoleUsers.OrderBy(x => x.RoleId).ToList().ForEach(RoleUser =>
        {

            var AreaDepartment = context.AreaDepartments.Where(x => x.RoleUserId == RoleUser.Id).Include(x => x.Department).FirstOrDefault();
            if (AreaDepartment?.Department is not null)
                RoleUserWithDepartment = RoleUser;
        });
        if (RoleUserWithDepartment is null)
            return null;

        var AreaDepartments = await context.AreaDepartments.Where(x => x.RoleUserId == RoleUserWithDepartment.Id).ToListAsync();
        var Department = await context.Departments.Where(x => x.Id == 1).FirstOrDefaultAsync();

        Department.AreaDepartments = AreaDepartments;

        var CurrentRole = await context.Roles.Where(x => x.Id == RoleUserWithDepartment.RoleId).FirstOrDefaultAsync();

        Info Infos = new()
        {
            BirthDate = MySqlUser.BirthDate,
            Id = MySqlUser.Id,
            AreaId = AreaInfo.Id,
            AuthId = authUser.Id,
            MapCenter = new MapCenter(AreaInfo.Center),
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
            Mobile = MySqlUser.Mobile,
            Name = MySqlUser.Name,
            Version = "V3",
            IsAdmin = CurrentRole?.Id == 2 || CurrentRole?.Id == 6,
            IsSuperAdmin = CurrentRole?.Id == 1
        };

        AuthInfoResponse authInfoResponse = new AuthInfoResponse
        {
            Infos = Infos
        };

        return authInfoResponse;

    }
}