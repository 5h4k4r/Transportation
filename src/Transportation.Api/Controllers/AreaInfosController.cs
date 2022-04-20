using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tranportation.Api.Responses;
using Transportation.Api.Extensions;
using Transportation.Api.Helpers;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;
using Transportation.Api.Models.Common;

namespace Transportation.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/area-infos")]
[Authorize]
public class AreaInfosController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;


    public AreaInfosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [ProducesResponseType(typeof(List<ListAreaInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> ListAreaInfos([FromServices] UserAuthContext authContext)
    {
        var user = authContext.GetAuthUser();
        var MySqlUser = await _unitOfWork.User.GetUserByAuthId(user.Id, true);

        if (MySqlUser is null)
            return BadRequest();

        var areaList = new List<AreaInfo>();

        if (MySqlUser.HasRole("superadmin"))
            areaList = await _unitOfWork.AreaInfos.ListAreaInfos();


        // TODO: this can be done with a join with area_infos table if area_id in employee table was foreign key

        var employee = await _unitOfWork.Employees.GetEmployeeByUserId(MySqlUser.Id);


        if (employee is null || !employee.AreaId.HasValue)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(employee), (int)MySqlUser.Id));

        areaList = await _unitOfWork.AreaInfos.GetAreaInfoById(employee.AreaId.Value);

        List<ListAreaInfoResponse> areasResponse = areaList.Select(x => new ListAreaInfoResponse
        {
            Id = x.Id,
            Title = x.Title,
        }).ToList();


        return Ok(areasResponse);



    }


}