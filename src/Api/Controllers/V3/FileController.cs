using System.Net.Mime;
using AutoMapper;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/files")]
public class FileController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public FileController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<PaginatedResponse<FileDto>>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> ListFile([FromQuery] ListFilesRequest request)
    {
        var files = await _unitOfWork.Files.ListFiles(request);
        var fileCounts = await _unitOfWork.Files.CountFile(request);
        if (!files.Any())
            return NotFound(new BasicResponse("No file found"));

        return Ok(new PaginatedResponse<FileDto>(fileCounts, request, files));
    }


    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<IActionResult> CreateFile(CreateFileRequest request)
    {
        await _unitOfWork.Files.CreateFile(request.Paths);
        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }

    [ProducesResponseType(typeof(ClientFileDto), StatusCodes.Status200OK)]
    [HttpPost("translate")]
    public async Task<IActionResult> CreateTranslateFile(CreateTranslateFileRequest request)
    {
        var createdFile = await _unitOfWork.Files.CreateTranslateFile(request);

        await _unitOfWork.Save();
        var response = _mapper.Map<ClientFileDto>(createdFile);

        return Ok(response);
    }
}