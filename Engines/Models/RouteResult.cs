using Itinero.LocalGeo;
using Shared.Models;

namespace DynamicSimulationConsole.Engines.Models;
public class RouteResult
{
    public Coordinate[] Coordinates;
    public OsmBridge[] Bridges;

    public RouteResult(Coordinate[] coordinates)
    {
        Coordinates = coordinates;
    }
}