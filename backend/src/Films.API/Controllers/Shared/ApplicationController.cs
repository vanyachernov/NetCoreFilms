using Films.API.Reponse;
using Microsoft.AspNetCore.Mvc;

namespace Films.API.Controllers.Shared;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        
        return new OkObjectResult(envelope);
    }
}