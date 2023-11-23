using System.Net;
using DynamicSimulationConsole.DataAccessLayer.Repositories;
using DynamicSimulationConsole.DataAccessLayer.Models;
using DynamicSimulationConsole.Services;
using DynamicSimulationConsole.Services.Models;
using DynamicSimulationConsole.WebApi.Models;
using Engines;
using Engines.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Models;

namespace DynamicSimulationConsole.WebApi;

[ApiController]
[Route("[controller]")]
public class SimulationController : ControllerBase
{
    private readonly ILogger<SimulationController> _logger;
    private readonly ISimulationEngine _simulationEngine;

    public SimulationController(ILogger<SimulationController> logger, ISimulationEngine simulationEngine)
    { 
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _simulationEngine = simulationEngine ?? throw new ArgumentNullException(nameof(simulationEngine));
    }

    [HttpPost("RunSimulation")]
    public async Task<IActionResult> RunSimulation([FromBody] SimulationParameters input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[POST]: RunSimulation");

        // input.StartLocation = new LatLng()
        // {
        //     lat = 41.332119,
        //     lon = -96.0333786
        // };
        //
        // input.EndLocation = new LatLng()
        // {
        //     lat = 41.32913110042719,
        //     lon = -96.03558199999999
        // };
        var route = _simulationEngine.Test(input.StartLocation, input.EndLocation , input.NumberOfRoutes);
        return Ok(route);
    }
    
} 