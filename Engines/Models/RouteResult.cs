using Shared.Models;

namespace DynamicSimulationConsole.Engines.Models;
public class RouteResult
{
    public Node[] Nodes;
    public OsmBridge[] Bridges;

    public RouteResult(Node[] nodes)
    {
        Nodes = nodes;
    }
}