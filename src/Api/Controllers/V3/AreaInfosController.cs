using System.Net.Mime;
using Api.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Requests;
using Infra.Entities;
using Infra.Entities.Common;
using Infra.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult> ListAreaInfos([FromQuery] ListAreaInfosRequest model, [FromServices] UserAuthContext authContext)
    {
        var user = authContext.GetAuthUser();
        var MySqlUser = await _unitOfWork.User.GetUserByAuthId(user.Id, true);

        if (MySqlUser is null)
            return BadRequest();

        var areaList = new List<AreaInfoDTO>();

        if (MySqlUser.HasRole("superadmin"))
            areaList = await _unitOfWork.AreaInfos.ListAreaInfos(model);

        else
        {


            // TODO: this can be done with a join with area_infos table if area_id in employee table was foreign key
            // Done!

            var employee = await _unitOfWork.Employees.GetEmployeeByUserId(MySqlUser.Id);


            if (employee is null || !employee.AreaId.HasValue)
                return Forbid();

            var areaInfo = employee.AreaInfo;

            if (areaInfo is not null)
                areaList = new List<AreaInfoDTO> { areaInfo };
        }

        List<ListAreaInfoResponse> areasResponse = areaList.Select(x => new ListAreaInfoResponse
        {
            Id = x.Id,
            Title = x.Title,
        }).ToList();


        return Ok(areasResponse);



    }


}