using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBD.Samples.ClientCertAuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InfoController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public InfoController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [HttpPost]
    public IHeaderDictionary Get() => Request.Headers;
}