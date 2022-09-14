using DynamicSimulationConsole.RoadGraph;
using DynamicSimulationConsole.RoadGraph.Repositories;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace DynamicSimulationConsole.WebApi
{
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
            var convoy = new Convoy();
            var guid = _repository.AddConvoy(convoy);
            return Ok(guid);
        }

        [HttpGet("GetConvoyById")]
        public IActionResult GetConvoyById([FromQuery] Guid id)
        {
            _logger.Log(LogLevel.Information, $"[GET]: GetConvoy");
            if (_repository.TryGetConvoyById(id, out var convoy))
            {
                return Ok(convoy);
            }

            return NotFound();
        }
        
    }
}