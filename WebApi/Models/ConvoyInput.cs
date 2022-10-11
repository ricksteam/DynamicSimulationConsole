using DynamicSimulationConsole.RoadGraph;

namespace DynamicSimulationConsole.WebApi.Models;

public class ConvoyInput
{
    public string ConvoyName { get; set; }
    public int startNodeId { get; set; }
    public int endNodeId { get; set; }
    
    public ConvoyVehicle[] ConvoyVehicles { get; set; }
}
