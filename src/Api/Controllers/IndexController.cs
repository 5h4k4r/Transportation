using System.Net.Mime;
using Api.Settings;
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
    private readonly IConfiguration _config;

    public IndexController(IConfiguration configuration)
    {
        _config = configuration;
    }


    [HttpGet("get-settings")]
    public IActionResult GetSettings()
    {
        var settings = _config.GetSection(SettingsConfig.Config).Get<AuthEndpoints>();


        return Ok(new { Data = settings });

    }
}