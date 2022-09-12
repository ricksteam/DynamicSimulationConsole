using DynamicSimulationConsole.RoadGraph;
using DynamicSimulationConsole.RoadGraph.Repositories;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSimulationConsole.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private readonly IRoadGraphRepository _repository;
        
        public SimulationController(ILogger<SimulationController> logger, IRoadGraphRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        
        [HttpPost("StartSimulation")]
        public IActionResult StartSimulation([FromBody] SimulationInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _logger.Log(LogLevel.Information, $"[POST]: Simulation/StartSimulation");
            var newGraph = new Graph(input.nodes, input.routes);
            var guid = _repository.AddGraph(newGraph);
            return Ok(guid);
        }
        
        [HttpDelete("StopSimulation")]
        public IActionResult StopSimulation([FromBody] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _logger.Log(LogLevel.Information, $"[DELETE]: Simulation/StopSimulation");
            var deleted = _repository.TryDeleteGraphById(id);
            return Ok(deleted);
        }

        [HttpGet("ShortestPath")]
        public IActionResult GetShortestPath([FromQuery] string id, [FromQuery] int startId, [FromQuery] int endId)
        {
            _logger.Log(LogLevel.Information, $"[GET]: Simulation/ShortestPath");
            if (!_repository.TryGetGraphById(new Guid(id), out var graph)) return NotFound("ID not found in repository");

            var shortestPath = graph.GetShortestPath(startId, endId);
            var str = "";
            foreach(var node in shortestPath)
            {
                str += $"Node {node.nodeId}\n";
            }
            
            return Ok(str);
        }
    }
}