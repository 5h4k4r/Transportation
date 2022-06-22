using System.Net.Mime;
using Api.Extensions;
using Core.Models.Authentication;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Authentication;
using Infra.Interfaces;
using Infra.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

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
    public async Task<ActionResult> ListAreaInfos([FromQuery] ListAreaInfosRequest model,
        [FromServices] UserAuthContext authContext)
    {
        var user = authContext.GetAuthUser();
        var mySqlUser = await _unitOfWork.User.GetUserByAuthId(user.Id, true);

        if (mySqlUser is null)
            return BadRequest();

        var areaList = new List<AreaInfoDto>();

        if (mySqlUser.HasRole(Roles.SuperAdmin))
        {
            areaList = await _unitOfWork.AreaInfos.ListAreaInfos(model);
        }

        else
        {
            
            // TODO: this can be done with a join with area_infos table if area_id in employee table was foreign key

            var employee = await _unitOfWork.Employees.GetEmployeeByUserId(mySqlUser.Id);


            if (employee is null || !employee.AreaId.HasValue)
                return Forbid();

            var areaInfo = await _unitOfWork.AreaInfos.GetAreaInfoById(employee.AreaId.Value);

            if (areaInfo is not null)
                areaList.Add(areaInfo);

            if (areaInfo is not null)
                areaList = new List<AreaInfoDto> { areaInfo };
        }

        var areasResponse = areaList.Select(x => new ListAreaInfoResponse
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();


        return Ok(areasResponse);
    }
}