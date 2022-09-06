using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.WebApi.Models
{
    [System.Serializable]
    public class SimulationInput
    {
        public Node[] Nodes { get; set; } 
        public Tuple<int, int>[] Routes { get; set; }
    }
}
