using System.Globalization;
using Engines;
using OsmSharp;
using OsmSharp.Complete;
using OsmSharp.Streams;
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
    private const double COORDINATE_PRECISION = 0.000001;
    
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

   

    [HttpPost("GetRouteBridges")]
    public ActionResult<IEnumerable<OsmBridge>> GetRouteBridges([FromBody] LatLng[] coordinates)
    {
        const double distanceThreshold = 0.1;
        var bridgeList = new List<OsmBridge>();

        for (var i = 0; i < coordinates.Length - 1; i++)
        {
            foreach (var bridge in _osmData.Bridges)
            {
                foreach (var node in bridge.Nodes)
                {
                    var distance = GeoEngine.CalculateMinimumDistance(coordinates[i], coordinates[i + 1], node);
                    if (distance <= distanceThreshold)
                    {
                        bridgeList.Add(bridge);
                        break;
                    }
                }
            }
        }

        return bridgeList;
    }

    [HttpPost("GetOsmNodes")]
    public ActionResult<IEnumerable<Node>> GetOsmNodes([FromBody] LatLng[] coordinates)
    {
        var coordinateSet = new HashSet<LatLng>(coordinates);
        var nodes = new List<Node>();
        using (var fileStream = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-merge-v1-1-1.pbf"))
        {
            using (var source = new PBFOsmStreamSource(fileStream))
            {
                foreach (var osmGeo in source)
                {
                    if (osmGeo.Type == OsmGeoType.Node)
                    {
                        var node = osmGeo as Node;
                        if (node == null) continue;
                        var nodeCoordinate = new LatLng()
                        {
                            lat = node.Latitude ?? 0,
                            lon = node.Longitude ?? 0
                        };

                        foreach (var coordinate in coordinates)
                        {
                            if (Math.Abs(nodeCoordinate.lat - coordinate.lat) <= COORDINATE_PRECISION &&
                                    Math.Abs(nodeCoordinate.lon - coordinate.lon) <= COORDINATE_PRECISION)
                            {
                                nodes.Add(node);
                                coordinateSet.Remove(nodeCoordinate);
                                if(coordinateSet.Count == 0) break;
                            }
                        }
                    }
                }
            }
        }
        // foreach (var coord in coordinates)
        // {
        //     // if (!(double.TryParse(coord.lat, out var latitude) && double.TryParse(coord.lon, out var longitude)))
        //     // {
        //     //     return BadRequest("Invalid latitude or longitude");
        //     // }
        //     var node = _osmData.Nodes.FirstOrDefault(n =>
        //         Math.Abs(n.Latitude - coord.lat) <= COORDINATE_PRECISION &&
        //         Math.Abs(n.Longitude - coord.lon) <= COORDINATE_PRECISION);
        //     response.Add(node);
        // }
        return nodes;
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