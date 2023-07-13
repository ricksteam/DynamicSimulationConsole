using System.Net;
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
public class AmbushAvoidanceController : ControllerBase
{
    private readonly ILogger<AmbushAvoidanceController> _logger;
    private readonly OsmData _osmData;
    
    public AmbushAvoidanceController(ILogger<AmbushAvoidanceController> logger, OsmData osmData)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _osmData = osmData;
    }

    [HttpPost("GenerateScores")]
    public async Task<IActionResult> GenerateScores([FromBody] ValhallaRouteParameters input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[POST]: GenerateScores");
        return Ok();
    }
}