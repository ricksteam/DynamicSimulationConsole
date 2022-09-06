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
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private static Graph s_graph;
        public SimulationController(ILogger<SimulationController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

     

        [HttpPost(Name = "NewSimulation")]
        public IActionResult NewSimulation([FromBody] SimulationInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            s_graph = new Graph(input.Nodes, input.Routes);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetShortestPath([FromQuery] int startId, [FromQuery] int endId)
        {
            var shortestPath = s_graph.GetShortestPath(startId, endId);
            var str = "";
            foreach(var node in shortestPath)
            {
                str += $"Node {node.NodeId}\n";
            }
            for (int i = 0; i < shortestPath.Count - 1; i++)
            {
                var node = shortestPath[i];
                var nodeNext = shortestPath[i + 1];

                var dist = node.Coordinate.DistanceTo(nodeNext.Coordinate);
                //str += $
            }
            return Ok(str);
        }
    }
}