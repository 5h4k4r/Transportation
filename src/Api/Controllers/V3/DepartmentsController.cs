// using System.Net.Mime;
// using AutoMapper;
// using Core.Helpers;
// using Core.Interfaces;
// using Core.Models.Base;
// using Core.Models.Common;

// using Core.Requests;
// using Infra.Entities;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace Api.Controllers;

// [Authorize]
// [ApiController]
// [Produces(MediaTypeNames.Application.Json)]
// [Consumes(MediaTypeNames.Application.Json)]
// [Route("v3/departments")]
// public class DepartmentsController : ControllerBase
// {

//     private readonly IUnitOfWork _unitOfWork;

//     private readonly IMapper _mapper;

//     public DepartmentsController(IUnitOfWork unitOfWork, IMapper mapper)
//     {
//         _unitOfWork = unitOfWork;
//         _mapper = mapper;
//     }

//     [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
//     [HttpGet]
//     public async Task<ActionResult> GetDepartments()
//     {

//         var dept = await _unitOfWork.Departments.GetDepartments();
//         if (dept is null) return NotFound(BasicResponse.ResourceDoesNotExist(nameof(dept)));
//         return Ok(dept);
//     }

//     [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
//     [HttpPost]

//     public async Task<ActionResult> CreateDepartment([FromBody] CreatedDepartmentRequest department)
//     {
//         _unitOfWork.Departments.CreateDepartment(department);

//         await _unitOfWork.Save();


//         return Ok(BasicResponse.Successful);
//     }
//     [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
//     [HttpDelete("{id}")]

//     public async Task<ActionResult> DeleteDepartment(uint id)
//     {
//         _unitOfWork.Departments.DeleteDepartment(id);

//         await _unitOfWork.Save();


//         return Ok(BasicResponse.Successful);
//     }
//     [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
//     [HttpPut("{id}")]

//     public async Task<ActionResult> UpdateDepartment([FromBody] CreatedDepartmentRequest department, uint id)
//     {

//         _unitOfWork.Departments.UpdateDepartment(department, id);

//         await _unitOfWork.Save();


//         return Ok(BasicResponse.Successful);
//     }

// }