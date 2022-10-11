using DynamicSimulationConsole.RoadGraph;
using DynamicSimulationConsole.RoadGraph.Repositories;
using DynamicSimulationConsole.Shared.Models;
using DynamicSimulationConsole.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSimulationConsole.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private readonly IRoadGraphRepository _graphRepository;
        private readonly IConvoyRepository _convoyRepository;
        
        public SimulationController(ILogger<SimulationController> logger, IRoadGraphRepository graphRepository, IConvoyRepository convoyRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _graphRepository = graphRepository ?? throw new ArgumentNullException(nameof(graphRepository));
            _convoyRepository = convoyRepository ?? throw new ArgumentNullException(nameof(convoyRepository));
        }

        
        [HttpPost("StartSimulation")]
        public IActionResult StartSimulation([FromBody] SimulationInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _logger.Log(LogLevel.Information, $"[POST]: Simulation/StartSimulation");
            var newGraph = new Graph(input.nodes, input.connections);
            var guid = _graphRepository.AddGraph(newGraph);
            return Ok(guid);
        }
        
        [HttpDelete("StopSimulation")]
        public IActionResult StopSimulation([FromBody] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _logger.Log(LogLevel.Information, $"[DELETE]: Simulation/StopSimulation");
            var deleted = _graphRepository.TryDeleteGraphById(id);
            return Ok(deleted);
        }

        [HttpGet("ShortestPath")]
        public IActionResult GetShortestPath([FromQuery] Guid routeId, [FromQuery] Guid convoyId)
        {
            _logger.Log(LogLevel.Information, $"[GET]: Simulation/ShortestPath");
            if (!_graphRepository.TryGetGraphById(routeId, out var graph)) return NotFound("ID not found in graph repository");
            if (!_convoyRepository.TryGetConvoyById(convoyId, out var convoy)) return NotFound("ID not found in convoy repository");
                
            var resultDict = new Dictionary<int, List<int>>();
            var threads = new Task[convoy.vehicles.Count];
            for (var i = 0; i < convoy.vehicles.Count; i++)
            {
                var vehicle = convoy.vehicles[i];
                threads[i] = Task.Factory.StartNew(() =>
                {
                    var resultPath = ConvoyVehicleThread(vehicle, convoy, graph);
                    resultDict.Add(vehicle.VehicleId, resultPath.Select(rp => rp.nodeId).ToList());
                });
            }

            Task.WaitAll(threads);
            
            return Ok(resultDict);
        }

        private List<PathNode> ConvoyVehicleThread(ConvoyVehicle vehicle, Convoy convoy, Graph graph)
        {
            return graph.GetShortestPath(convoy.startPositionId, convoy.endPositionId, vehicle);
        }
    }
}