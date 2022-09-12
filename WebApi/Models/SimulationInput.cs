// ReSharper disable UnusedAutoPropertyAccessor.Global
using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.WebApi.Models
{
    public class SimulationInput
    {
        public Node[] nodes { get; set; }
        public Tuple<int, int>[] routes { get; set; }  
    }
}
