using System.Net.Mime;
using Core.Index;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

//create a controller for get-settings endpoint
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("")]
public class IndexController : ControllerBase
{
    [HttpGet("get-settings")]
    public IActionResult GetSettings()
    {
        List<Setting> Exclude = new()
        {
            new Setting { Address = "/swagger", Method = "get" }
        };


        return Ok(new GetSettings
        {
            Exclude = Exclude,
            Block = new List<Setting>()
        });

    }
}