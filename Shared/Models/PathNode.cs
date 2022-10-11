
namespace DynamicSimulationConsole.Shared.Models
{
    [Serializable]
    public class PathNode : Node
    {
        public double gCost;
        public double hCost;
        public double fCost;
        
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
        
        public bool IsValidNode(float speed, float weight)
        {
            //var speedLimit = GetSpeedLimitMph();
            var weightLimit = GetWeightLimitKg();

            if (weight >= weightLimit) return false;
            //if (speed <= speedLimit) return false;

            return true; 
        }
    } 
}
