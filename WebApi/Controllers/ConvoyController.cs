using DynamicSimulationConsole.DataAccessLayer.Repositories;
using DynamicSimulationConsole.DataAccessLayer.Models;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicSimulationConsole.WebApi;

[ApiController]
[Route("[controller]")]
public class ConvoyController : ControllerBase
{
    private readonly ILogger<ConvoyController> _logger;
    private readonly IConvoyRepository _repository;

    public ConvoyController(ILogger<ConvoyController> logger, IConvoyRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpPost("NewConvoy")]
    public IActionResult NewConvoy([FromBody] ConvoyInput input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[POST]: NewConvoy");
        var convoy = new Convoy(input.ConvoyName, input.Vehicles);
        _repository.AddConvoy(convoy);
        return Ok();
    }

    [HttpGet("GetAllConvoys")]
    public IActionResult GetAllConvoys()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _logger.Log(LogLevel.Information, $"[GET]: GetAllConvoys");
        var convoys = _repository.GetAllConvoys();
        return Ok(JsonConvert.SerializeObject(convoys));
    }

    [HttpDelete("DeleteConvoy")]
    public IActionResult DeleteConvoy([FromQuery] Guid id)
    {
        _logger.Log(LogLevel.Information, $"[DELETE]: DeleteConvoy");
        if (_repository.TryDeleteConvoyById(id))
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpGet("GetConvoyById")]
    public IActionResult GetConvoyById([FromQuery] Guid id)
    {
        _logger.Log(LogLevel.Information, $"[GET]: GetConvoy");
        if (_repository.TryGetConvoyById(id, out var convoy))
        {
            return Ok(JsonConvert.SerializeObject(convoy));
        }

        return NotFound();
    }
}
