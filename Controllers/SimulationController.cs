
using DynamicSimulationConsole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DynamicSimulationConsole.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private Graph _g;
        public SimulationController(ILogger<SimulationController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

     

        [HttpPost(Name = "NewSimulation")]
        public IActionResult NewSimulation([FromBody] SimulationInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var str = "";
            
            for (var i = 0; i < input.Nodes.Length - 1; i++)
            {
                var node = input.Nodes[i];
                var nodeN = input.Nodes[i + 1];

                var dist1 = node.Coordinate.DistanceTo(nodeN.Coordinate);

                str += $"Dist from node {i + 1} to node {i + 2} = {dist1}\n";
            }

            _g = new Graph(input);
            
            //var testA = input.Nodes[0];
            //var testB = input.Nodes[1];

            //var nodeCoord1 = testA.Coordinate;
            //var nodeCoord2 = testB.Coordinate;

            //var dist = nodeCoord1.DistanceFrom(nodeCoord2);
            return Ok(str);
        }

        [HttpGet]
        public IActionResult GetNode([FromQuery] int id)
        {
            var node = _g.GetNodeById(1);
            return Ok(JsonConvert.SerializeObject(node));
        }
    }
}