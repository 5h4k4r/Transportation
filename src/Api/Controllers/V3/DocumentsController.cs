using System.Net.Mime;
using Api.Extensions;
using Core.Models.Authentication;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Requests;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/documents")]
public class DocumentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DocumentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("verify")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifyDocuments(VerifyDocumentsRequest model)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();

        var databaseDocuments =
            await _unitOfWork.Document.ListDocumentsByIds(model.documentIds);

        if (databaseDocuments is null)
            throw new NotFoundException("No documents found");

        foreach (var document in databaseDocuments) document.IsVerified = true;

        _unitOfWork.Document.UpdateDocuments(databaseDocuments);
        await _unitOfWork.Save();
        return Ok(databaseDocuments);
    }
}