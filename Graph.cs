using DynamicSimulationConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSimulationConsole
{
    public class Graph
    {
        private List<Node> _nodes;
        private List<Tuple<int, int>> _routes;
        
        
        public Graph(SimulationInput input)
        {
            _nodes = new List<Node>(input.Nodes);
            _routes = new List<Tuple<int, int>>(input.Routes);
        }

        public List<Node> GetNodesByType(NodeType type)
        {
            return _nodes.Where(x => x.NodeType == type).ToList();
        }
        public bool TryGetNodesByType(NodeType type, out List<Node> nodes)
        {
            nodes = GetNodesByType(type);
            return nodes != null;
        }
        public Node GetNodeById(int id)
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
    }
}
