using DynamicSimulationConsole.DataAccessLayer.Repositories;
using DynamicSimulationConsole.DataAccessLayer.Models;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Route = DynamicSimulationConsole.DataAccessLayer.Models.Route;

namespace DynamicSimulationConsole.WebApi;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;
    private readonly IRouteRepository _repository;

    public RouteController(ILogger<RouteController> logger, IRouteRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpPost("NewRoute")]
    public IActionResult NewRoute([FromBody] RouteInput input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[POST]: NewRoute");
        var route = new Route(input.RouteName);
        _repository.AddRoute(route);
        return Ok();
    }

    [HttpGet("GetAllRoutes")]
    public IActionResult GetAllConvoys()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[GET]: GetAllRoutes");
        var routes = _repository.GetAllRoutes();
        return Ok(JsonConvert.SerializeObject(routes));
    }

    [HttpDelete("DeleteRoute")]
    public IActionResult DeleteRoute([FromQuery] Guid id)
    {
        _logger.Log(LogLevel.Information, $"[DELETE]: DeleteRoute");
        if (_repository.TryDeleteRouteById(id))
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpGet("GetRouteById")]
    public IActionResult GetRouteById([FromQuery] Guid id)
    {
        _logger.Log(LogLevel.Information, $"[GET]: GetRouteById");
        if (_repository.TryGetRouteById(id, out var route))
        {
            return Ok(JsonConvert.SerializeObject(route));
        }

        return NotFound();
    }
}
