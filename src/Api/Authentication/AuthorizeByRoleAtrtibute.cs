using Api.Extensions;
using Core.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Authentication;

//Extending from AuthorizeAttribute or Attribute is upto user choice.
//You can consider using AuthorizeAttribute if you want to use the predefined properties and functions from Authorize Attribute.
public class AuthorizeByRole : AuthorizeAttribute, IAuthorizationFilter
{
    private new string? Roles { get; set; } //Permission string to get from controller

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //Validate if any Roles are passed when using attribute at controller or action level
        if (string.IsNullOrEmpty(Roles))
        {
            //Validation cannot take place without any Roles so returning unauthorized
            context.Result = new UnauthorizedResult();
            return;
        }


        //The below line can be used if you are reading Roles from token
        //var RolesFromToken=context.HttpContext.User.Claims.Where(x=>x.Type=="Roles").Select(x=>x.Value).ToList()

        //Identity.Name will have windows logged in user id, in case of Windows Authentication
        //Identity.Name will have user name passed from token, in case of JWT Authentication and having claim type "ClaimTypes.Name"
        // var assignedPermissionsForUser = MockData.UserPermissions.Where(x => x.Key == userName).Select(x => x.Value).ToList();
        var roles = context.HttpContext.User.GetRoles();
        var requiredRoles =
            Roles.Split(",")
                .ToList(); //Multiple permission can be received from controller, delimiter "," is used to get individual values
        var index = 0;
        for (; index < requiredRoles.Count; index++)
        {
            var requiredRole = requiredRoles[index];
            if (roles.Any(y => y == (byte)(Role)Enum.Parse(typeof(Role), requiredRole)))
                return; //User Authorized. Without setting any result value and just returning is sufficient for authorizing user
        }

        context.Result = new UnauthorizedResult();
    }
}