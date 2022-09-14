using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.RoadGraph
{
    public class Graph
    {
        public Guid id;   
        private readonly List<PathNode> _nodes;
        private readonly List<Tuple<int, int>> _connections; 
     
        public Graph(IEnumerable<Node> nodes, IEnumerable<Tuple<int, int>> connections)
        {
            _nodes = new List<PathNode>(GetPathNodes(nodes));  
            _connections = new List<Tuple<int, int>>(connections);
        }

        private static IEnumerable<PathNode> GetPathNodes(IEnumerable<Node> nodes)
        {
            return nodes.Select((node) => new PathNode(node)).ToArray();
        }

        private List<Node> GetNodesByType(NodeType type)
        {
            return _nodes.Where(x => x.nodeType == type).ToList<Node>();
        }
        
        public bool TryGetNodesByType(NodeType type, out List<Node> nodes)
        {
            nodes = GetNodesByType(type);
            return nodes != null;
        }

        private PathNode GetNodeById(int id)
        {
            return _nodes.FirstOrDefault(x => x.nodeId == id);
        }

        private bool TryGetNodeById(int id, out Node node)
        {
            node = GetNodeById(id);
            return node != null;
        }
        public List<Tuple<int, int>> GetRoutesByNodeId(int id)
        {
            return _connections.Where(x => x.Item1 == id || x.Item2 == id).ToList();
        }

        private List<PathNode> GetAllNodesConnectedTo(int nodeId)
        {
            var routes = _connections.Where(x => x.Item1 == nodeId).ToList();
            return routes.Select(route => GetNodeById(route.Item2)).ToList();   
        }

        public List<PathNode> GetShortestPath(int nodeIdStart, int nodeIdEnd)
        {
            var _openList = new List<PathNode>();
            var _closedList = new List<PathNode>();

            
            if (TryGetNodeById(nodeIdStart, out var nodeStart) && TryGetNodeById (nodeIdEnd, out var nodeEnd))
            {
                var pathNodeStart = new PathNode(nodeStart);
                var pathNodeEnd = new PathNode(nodeEnd);

                _openList.Add(pathNodeStart);

                foreach (var node in _nodes)
                {
                    node.gCost = int.MaxValue;
                    node.CalculateFCost();
                    node.previousNode = null;
                }

                pathNodeStart.gCost = 0;
                pathNodeStart.hCost = pathNodeStart.coordinate.DistanceTo(pathNodeEnd.coordinate);
                pathNodeStart.CalculateFCost();

                while(_openList.Count > 0)
                {
                    var currentNode = GetLowestFCostNode(_openList);
                    var newEnd = GetNodeById(nodeIdEnd);
                    if (currentNode.nodeId == pathNodeEnd.nodeId) return CalculatePath(newEnd);
                    _openList.Remove(currentNode);
                    _closedList.Add(currentNode);

                    foreach(var neighbourNode in GetAllNodesConnectedTo(currentNode.nodeId))
                    {
                        if (neighbourNode.obstructed) continue;
                       // if (neighbourNode.)
                        if (_closedList.Contains(neighbourNode)) continue;

                        var tentativeGCost = currentNode.gCost + currentNode.coordinate.DistanceTo(neighbourNode.coordinate);
                        if (tentativeGCost < neighbourNode.gCost)
                        {
                            neighbourNode.previousNode = currentNode;
                            neighbourNode.gCost = tentativeGCost;
                            neighbourNode.hCost = neighbourNode.coordinate.DistanceTo(pathNodeEnd.coordinate);
                            neighbourNode.CalculateFCost();

                            if (!_openList.Contains(neighbourNode))
                            {
                                _openList.Add(neighbourNode);
                            }
                        }
                    }
                }

                return null;
            }
            return null;
           
        }
        
        
        private static List<PathNode> CalculatePath(PathNode endNode)
        {
            var path = new List<PathNode> { endNode };
            var currentNode = endNode;

            while(currentNode.previousNode != null)
            {
                path.Add(currentNode.previousNode);
                currentNode = currentNode.previousNode;
            }

            path.Reverse();
            return path;
        }


        private static PathNode GetLowestFCostNode(List<PathNode> pathNodes)
        {
            var lowestFCostNode = pathNodes[0];
            foreach (var pathNode in pathNodes.Where(pathNode => pathNode.fCost < lowestFCostNode.fCost))
            {
                lowestFCostNode = pathNode;
            }
            return lowestFCostNode;
        }

        public double GetDistanceBetweenTwoNodes(int nodeId1, int nodeId2)
        {
            if (TryGetNodeById(nodeId1, out var node1) && TryGetNodeById(nodeId2, out var node2))
            {
                return node1.coordinate.DistanceTo(node2.coordinate);
            }

            return -1;
        }
    }
}
