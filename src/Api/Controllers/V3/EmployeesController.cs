using System.Net.Mime;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Authentication;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Api.Controllers.V3;

[ApiController]
[Authorize]
[Route("v3/employees")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class EmployeesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpPatch("change-language")]
    public async Task<IActionResult> ChangeLanguage(ChangeEmployeeLanguageRequest model,
        [FromServices] UserAuthContext authContext)
    {
        var authUser = authContext.GetAuthUser();

        var user = await _unitOfWork.User.GetUserByAuthId(authUser.Id);

        if (user is null)
            return NotFound(BasicResponse.ResourceNotFound);

        model.UserId = user.Id;

        var employee = await _unitOfWork.Employees.GetEmployeeByUserId(model.UserId);

        if (employee is null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(employee), (int)model.UserId));


        try
        {
            _unitOfWork.Employees.ChangeEmployeeLanguage(model);
            await _unitOfWork.Save();

            return Ok(BasicResponse.Successful);
        }
        catch (Exception e)
        {
            var sqlException = _unitOfWork.GetException<MySqlException>(e);

            return BadRequest(sqlException?.Message ?? e.Message);
        }
    }
}