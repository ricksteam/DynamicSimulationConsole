using DynamicSimulationConsole.DataAccessLayer.Repositories;
using DynamicSimulationConsole.DataAccessLayer.Models;
using DynamicSimulationConsole.Services;
using DynamicSimulationConsole.Services.Models;
using DynamicSimulationConsole.WebApi.Models;
using Engines;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Models;

namespace DynamicSimulationConsole.WebApi;

[ApiController]
[Route("[controller]")]
public class ValhallaController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ValhallaController> _logger;
    private readonly ValhallaClient _valhallaClient;
    private readonly OsmData _osmData;
    
    public ValhallaController(ILogger<ValhallaController> logger, IConfiguration configuration, OsmData osmData)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _valhallaClient = new ValhallaClient(_configuration.GetSection("AppSettings")["ValhallaURI"]);
        _osmData = osmData;
    }

    [HttpPost("GetValhallaRoute")]
    public async Task<IActionResult> GetValhallaRoute([FromBody] ValhallaRouteParameters input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[POST]: GetRoute");
        var route = await _valhallaClient.GetRoute(input);
        return Ok(route);
    }
}