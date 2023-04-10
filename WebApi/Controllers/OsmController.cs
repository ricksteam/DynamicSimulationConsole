using System.Globalization;
using Shared.Models;

namespace DynamicSimulationConsole.WebApi;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class OsmController : ControllerBase
{
    private readonly OsmData _osmData;

    public OsmController(OsmData osmData)
    {
        _osmData = osmData;
    }
    
    [HttpGet("GetNodesInRadius")]
    public ActionResult<IEnumerable<OsmNode>> GetNodesByRadius(string lat, string lng, double radius = 0.01)
    {
        if (!double.TryParse(lat, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) ||
            !double.TryParse(lng, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
        {
            return BadRequest("Invalid latitude or longitude");
        }
        
        var nodes = _osmData.Nodes.Where(n =>
            n.Latitude >= latitude - radius &&
            n.Latitude <= latitude + radius &&
            n.Longitude >= longitude - radius &&
            n.Longitude <= longitude + radius
        ).ToList();

        return Ok(nodes);
    }
    
    
    [HttpGet("GetAllBridges")]
    public ActionResult<IEnumerable<OsmBridge>> GetAllBridges()
    {
        return Ok(_osmData.Bridges);
    }
    
    [HttpGet("Nodes/{id}")]
    public ActionResult<OsmNode> GetNodeById(long id)
    {
        var node = _osmData.Nodes.FirstOrDefault(n => n.Id == id);

        if (node == null)
        {
            return NotFound("Node not found");
        }

        return Ok(node);
    }
}