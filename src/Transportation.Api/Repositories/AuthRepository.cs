using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Common;
using Transportation.Api.Model;
using Transportation.Api.Requests;
using Transportation.Api.Responses;


namespace Transportation.Api.Repositories;

public class AuthRepository : IAuthRepository
{

    protected transportationContext _context;
    protected readonly ILogger<AuthRepository> _logger;

    public AuthRepository(ILogger<AuthRepository> logger, transportationContext context)
    {
        _logger = logger;
        _context = context;
    }

    public Task<AuthCheckResponse?> Check()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthInfoResponse?> GetAuthInfo(UserAuthContext authContext)
    {
        var authUser = authContext.GetAuthUser();
        var MySqlUser = await _context.Users
                                    .Where(x => x.AuthId == authUser.Id)
                                    .FirstOrDefaultAsync();

        if (MySqlUser is null)
            return null;

        var AreaInfo = await _context.AreaInfos.Where(x => x.Id == MySqlUser.AreaId).FirstOrDefaultAsync();
        var Employee = await _context.Employees.Where(x => x.UserId == MySqlUser.Id).FirstOrDefaultAsync();
        var RoleUsers = await _context.RoleUsers.Where(x => x.UserId == MySqlUser.Id).ToListAsync();

        RoleUser RoleUserWithDepartment = null;
        RoleUsers.OrderBy(x => x.RoleId).ToList().ForEach(RoleUser =>
        {

            var AreaDepartment = _context.AreaDepartments.Where(x => x.RoleUserId == RoleUser.Id).Include(x => x.Department).FirstOrDefault();
            if (AreaDepartment?.Department is not null)
                RoleUserWithDepartment = RoleUser;
        });
        if (RoleUserWithDepartment is null)
            return null;

        var AreaDepartments = await _context.AreaDepartments.Where(x => x.RoleUserId == RoleUserWithDepartment.Id).ToListAsync();
        var Department = await _context.Departments.Where(x => x.Id == 1).FirstOrDefaultAsync();

        Department.AreaDepartments = AreaDepartments;

        var CurrentRole = await _context.Roles.Where(x => x.Id == RoleUserWithDepartment.RoleId).FirstOrDefaultAsync();

        AuthInfoResponse authInfoResponse = new AuthInfoResponse
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

        return authInfoResponse;
    }

    public async Task<object?> Login(LoginRequest model)
    {

        string phone = PreparePhoneNumber(model.Mobile);
        var user = await _context.Users.Where(x => x.Mobile == phone).FirstOrDefaultAsync();

        if (user is null)
            return ErrorCode.ResourceDoesNotExist;

        user.AuthId = model.AuthId;

        _context.SaveChanges();

        return null;

    }

    public string PreparePhoneNumber(string model)
    {
        if (model.Substring(0, 1) != "+")
            model = "+" + model;


        return model;
    }

}