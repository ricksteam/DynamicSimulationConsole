using DynamicSimulationConsole.Engines.Models;
using Engines.Interface;
using Shared.Models;

namespace Engines.Implementation;

public class PbiData
{
    public Dictionary<long?, OsmSharp.Node> Nodes { get; set; }
    public List<Edge> Edges { get; set; }
}

public class SimulationEngineOld : ISimulationEngine
{
    private PbiData _osmData;
    
    public Dictionary<(long, long), double> edgePenalties = new Dictionary<(long, long), double>();
    
    public SimulationEngineOld(PbiData data)
    {
        _osmData = data;
    }
    public RouteResult[] Test(LatLng startPoint, LatLng endPoint, int numberOfRoutes)
    {
        var res = GenerateMultipleRoutes(_osmData.Nodes, _osmData.Edges, 492045288, 2386601331, 5, 1);
        return null;
    }
    
    public List<List<long>> GenerateMultipleRoutes(Dictionary<long?, OsmSharp.Node> nodes, List<Edge> edges, long startNodeId, long endNodeId, int numberOfRoutes, double penaltyAmount)
    {
        var routes = new List<List<long>>();
        
        for(var i = 0; i < numberOfRoutes; i++)
        {
            var route = Dijkstra(nodes, edges, startNodeId, endNodeId);
            if (route != null)
            {
                routes.Add(route);
                ApplyPenaltyToPath(route, penaltyAmount);
            }
        }
        return routes;
    }

    public void ApplyPenaltyToPath(List<long> path, double penaltyAmount)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            var edge = (path[i], path[i + 1]);
            if (edgePenalties.ContainsKey(edge))
            {
                edgePenalties[edge] += penaltyAmount;
            }
            else
            {
                edgePenalties[edge] = penaltyAmount;
            }
        }
    }
    
    public List<long> Dijkstra(Dictionary<long?, OsmSharp.Node> nodes, List<Edge> edges, long startNodeId, long endNodeId)
    {
        var distances = new Dictionary<long, double>();
        var previousNodes = new Dictionary<long, long>();
        var unvisited = new HashSet<long>();

        foreach (var node in nodes)
        {
            if (!node.Key.HasValue) continue;
            
            distances[node.Key.Value] = double.PositiveInfinity;
            unvisited.Add(node.Key.Value);
        }

        distances[startNodeId] = 0;

        while (unvisited.Count > 0)
        {
            var currentNode = GetNodeWithSmallestDistance(unvisited, distances);
            unvisited.Remove(currentNode);

            if (currentNode == endNodeId)
            {
                return BuildPath(previousNodes, endNodeId);
            }

            var neighborEdges = edges.FindAll(e => e.StartNodeId == currentNode);
            foreach (var edge in neighborEdges)
            {
                var neighbor = edge.EndNodeId;
                if (unvisited.Contains(neighbor))
                {
                    var edgePenalty = edgePenalties.GetValueOrDefault<(long, long), double>((edge.StartNodeId, edge.EndNodeId), 0);
                    var newDist = distances[currentNode] + edge.Weight + edgePenalty;

                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previousNodes[neighbor] = currentNode;
                    }
                }
            }
        }

        return null; // Path not found
    }

    private long GetNodeWithSmallestDistance(HashSet<long> unvisited, Dictionary<long, double> distances)
    {
        double smallestDistance = double.PositiveInfinity;
        long nodeIdWithSmallestDistance = 0;

        foreach (var nodeId in unvisited)
        {
            if (distances[nodeId] < smallestDistance)
            {
                smallestDistance = distances[nodeId];
                nodeIdWithSmallestDistance = nodeId;
            }
        }

        return nodeIdWithSmallestDistance;
    }

    private List<long> BuildPath(Dictionary<long, long> previousNodes, long endNodeId)
    {
        var path = new List<long>();
        var currentNodeId = endNodeId;

        while (previousNodes.ContainsKey(currentNodeId))
        {
            path.Insert(0, currentNodeId);
            currentNodeId = previousNodes[currentNodeId];
        }

        path.Insert(0, currentNodeId);
        return path;
    }
}