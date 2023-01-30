using DynamicSimulationConsole.DataAccessLayer.Repositories;
using DynamicSimulationConsole.DataAccessLayer.Models;
using DynamicSimulationConsole.Services;
using DynamicSimulationConsole.Services.Models;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicSimulationConsole.WebApi;

[ApiController]
[Route("[controller]")]
public class ValhallaController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ValhallaController> _logger;
    private readonly ValhallaClient _valhallaClient;

    public ValhallaController(ILogger<ValhallaController> logger, IConfiguration configuration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _valhallaClient = new ValhallaClient(_configuration.GetSection("AppSettings")["ValhallaURI"]);
    }

    [HttpPost("GetRoute")]
    public async Task<IActionResult> GetRoute([FromBody] ValhallaRouteParameters input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[POST]: GetRoute");
        var test = await _valhallaClient.GetRoute(input);
        return Ok(test);
    }
}