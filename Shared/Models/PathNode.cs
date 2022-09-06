using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSimulationConsole.Shared.Models
{
    public class PathNode : Node 
    {
        public double gCost;
        public double hCost;
        public double fCost;

        public bool obstructed;
        
        public PathNode previousNode;

        public PathNode(Node node)
        {
            NodeId = node.NodeId;
            NodeType = node.NodeType;
            Coordinate = node.Coordinate;
            gCost = 0;
            hCost = 0;
            fCost = 0;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    } 
}
