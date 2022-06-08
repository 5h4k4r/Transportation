
using System.Net.Mime;
using System.Text.Json;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/discount-codes")]
public class DiscountCodesController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DiscountCodesController(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(PaginatedResponse<DiscountCodeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult> ListDiscountCode([FromQuery] DiscountCodeRequest model)
    {


        var discountCode = await _unitOfWork.DiscountCode.ListDiscountCodes(model);
        var count = await _unitOfWork.DiscountCode.ListDiscountCodeCount(model);

        return Ok(new PaginatedResponse<DiscountCodeDto>(count,model,discountCode));
    }
    [ProducesResponseType(typeof(List<DiscountCodeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}/detail")]
    public async Task<ActionResult> DiscountCodeDetail(uint id)
    {


        var discountCode = await _unitOfWork.DiscountCode.DiscountCodeDetail(id);
       
        return Ok(discountCode);
    }
    [ProducesResponseType(typeof(PaginatedResponse<DiscountCodeUserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{codeId}/users")] //{codeId}/
    public async Task<ActionResult> ListUsers([FromQuery] DiscountCodeRequest model ,  uint codeId)
    {


        var users = await _unitOfWork.DiscountCode.ListUsers(model,codeId);
        var count = await _unitOfWork.DiscountCode.ListDiscountCodeUsersCount(model, codeId);

        return Ok(new PaginatedResponse<DiscountCodeUserDto>(count,model,users.DiscountCodeUser, new
        {
            TotalAmount = users.TotalAmount,
            TotalCount = users.TotalCount
        }));
    } 
    [ProducesResponseType(typeof(List<DiscountCodeUserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{codeId}/user/{userId}/tasks")]
    public async Task<ActionResult> ListUserTasks([FromQuery] DiscountCodeRequest model ,  uint codeId, uint userId)
    {
         var users = await _unitOfWork.DiscountCode.ListTasksByUser(model,codeId,userId);
         return Ok(new PaginatedResponse<DiscountCodeUserDto>(0,model,users.DiscountCodeUser,users.DiscountCode));
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]

    public IActionResult CreateDiscountCode([FromBody] CreateDiscountCodeRequest request)
    {
        var stringDetail = JsonSerializer.Serialize(request.Detail);
        var discountCode = _mapper.Map<DiscountCodeDto>(request);
        discountCode.Detail = stringDetail;
        _unitOfWork.DiscountCode.CreateDiscountCode(discountCode);
        _unitOfWork.Save();
        return Ok(BasicResponse.Successful);
    }

   


}