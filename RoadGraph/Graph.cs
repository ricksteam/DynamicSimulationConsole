using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.RoadGraph
{
    public class Graph
    {
        private List<PathNode> _nodes;
        private List<Tuple<int, int>> _routes;

        private List<PathNode> _openList;
        private List<PathNode> _closedList;

        public Graph(Node[] nodes, Tuple<int, int>[] routes)
        {
            _nodes = new List<PathNode>(GetPathNodes(nodes));  
            _routes = new List<Tuple<int, int>>(routes);
        }

        private PathNode[] GetPathNodes(Node[] nodes)
        {
            return nodes.Select((node) => new PathNode(node)).ToArray();
        } 
        public List<Node> GetNodesByType(NodeType type)
        {
            return _nodes.Where(x => x.NodeType == type).ToList<Node>();
        }
        public bool TryGetNodesByType(NodeType type, out List<Node> nodes)
        {
            nodes = GetNodesByType(type);
            return nodes != null;
        }
        public PathNode GetNodeById(int id)
        {
            return _nodes.FirstOrDefault(x => x.NodeId == id);
        } 
        public bool TryGetNodeById(int id, out Node node)
        {
            node = GetNodeById(id);
            return node != null;
        }
        public List<Tuple<int, int>> GetRoutesByNodeId(int id)
        {
            return _routes.Where(x => x.Item1 == id || x.Item2 == id).ToList();
        }

        public List<PathNode> GetAllNodesConnectedTo(int nodeId)
        {
            var routes = _routes.Where(x => x.Item1 == nodeId).ToList();
            return routes.Select(route => GetNodeById(route.Item2)).ToList();   
        }

        public List<PathNode> GetShortestPath(int nodeIdStart, int nodeIdEnd)
        {
            _openList = new List<PathNode>();
            _closedList = new List<PathNode>();

            
            if (TryGetNodeById(nodeIdStart, out var nodeStart) && TryGetNodeById (nodeIdEnd, out var nodeEnd))
            {
                var pathNodeStart = new PathNode(nodeStart);
                var pathNodeEnd = new PathNode(nodeEnd);

                _openList.Add(pathNodeStart);

                foreach (PathNode node in _nodes)
                {
                    node.gCost = int.MaxValue;
                    node.CalculateFCost();
                    node.previousNode = null;
                }

                pathNodeStart.gCost = 0;
                pathNodeStart.hCost = pathNodeStart.Coordinate.DistanceTo(pathNodeEnd.Coordinate);
                pathNodeStart.CalculateFCost();

                while(_openList.Count > 0)
                {
                    var currentNode = GetLowestFCostNode(_openList);
                    var newEnd = GetNodeById(nodeIdEnd);
                    if (currentNode.NodeId == pathNodeEnd.NodeId) return CalculatePath(newEnd);
                    _openList.Remove(currentNode);
                    _closedList.Add(currentNode);

                    foreach(var neighbourNode in GetAllNodesConnectedTo(currentNode.NodeId))
                    {
                        if (neighbourNode.obstructed) continue;
                        if (_closedList.Contains(neighbourNode)) continue;

                        var tentativeGCost = currentNode.gCost + currentNode.Coordinate.DistanceTo(neighbourNode.Coordinate);
                        if (tentativeGCost < neighbourNode.gCost)
                        {
                            neighbourNode.previousNode = currentNode;
                            neighbourNode.gCost = tentativeGCost;
                            neighbourNode.hCost = neighbourNode.Coordinate.DistanceTo(pathNodeEnd.Coordinate);
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
        
        
        private List<PathNode> CalculatePath(PathNode endNode)
        {
            var path = new List<PathNode>();
            path.Add(endNode);
            var currentNode = endNode;

            while(currentNode.previousNode != null)
            {
                path.Add(currentNode.previousNode);
                currentNode = currentNode.previousNode;
            }

            path.Reverse();
            return path;
        }


        private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
        {
            var lowestFCostNode = pathNodes[0];
            foreach(var pathNode in pathNodes)
            {
                if (pathNode.fCost < lowestFCostNode.fCost) lowestFCostNode = pathNode;
            }
            return lowestFCostNode;
        }

        public double GetDistanceBetweenTwoNodes(int nodeId1, int nodeId2)
        {
            if (TryGetNodeById(nodeId1, out var node1) && TryGetNodeById(nodeId2, out var node2))
            {
                return node1.Coordinate.DistanceTo(node2.Coordinate);
            }

            return -1;
        }
    }
}
