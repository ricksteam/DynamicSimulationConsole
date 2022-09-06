using DynamicSimulationConsole.RoadGraph;
using DynamicSimulationConsole.Shared.Models;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DynamicSimulationConsole.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class ConvoyController : ControllerBase
    {
        private readonly ILogger<ConvoyController> _logger;
        
        public ConvoyController(ILogger<ConvoyController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
 
        [HttpPost(Name = "NewConvoy")]
        public IActionResult NewConvoy([FromBody] ConvoyInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok();
        }
        
    }
}