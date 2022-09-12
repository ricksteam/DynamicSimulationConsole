
namespace DynamicSimulationConsole.Shared.Models
{
    [Serializable]
    public class PathNode : Node
    {
        public double gCost;
        public double hCost;
        public double fCost;

        public bool obstructed;
        
        public PathNode previousNode;

        public PathNode(Node node)
        {
            nodeId = node.nodeId;
            nodeType = node.nodeType;
            coordinate = node.coordinate;
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
