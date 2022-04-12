using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportation.Api.Interfaces;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/servants/performance")]
public class ServantsPerformanceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ServantsPerformanceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}