using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;


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
 
        [HttpPost("NewConvoy")]
        public IActionResult NewConvoy([FromBody] ConvoyInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _logger.Log(LogLevel.Information, $"[POST]: NewConvoy");
            
            return Ok();
        }
        
    }
}