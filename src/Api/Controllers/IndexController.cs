using System.Net.Mime;
using Core.Models.Index;
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
        List<AuthEndpoint> exclude = new()
        {
            new AuthEndpoint { Address = "/v3/auth/check", Method = "get" },
            new AuthEndpoint { Address = "/get-settings", Method = "get" },
            new AuthEndpoint { Address = "get-settings", Method = "get" },
            new AuthEndpoint { Address = "/swagger", Method = "get" },
            new AuthEndpoint { Address = "swagger", Method = "get" },
            new AuthEndpoint { Address = "/swagger/index.html", Method = "get" },
            new AuthEndpoint { Address = "swagger/index.html", Method = "get" },
            new AuthEndpoint { Address = "/v3", Method = "get" }
        };
        
        return Ok(new
        {
            Data = new AuthEndpoints
            {
                Exclude = new List<AuthEndpoint>(),
                Block = new List<AuthEndpoint>()
            }
        });
    }
}