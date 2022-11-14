using DynamicSimulationConsole.RoadGraph;

namespace DynamicSimulationConsole.WebApi.Models;

public class ConvoyInput
{
    public string ConvoyName { get; set; }
    public ConvoyVehicle[] ConvoyVehicles { get; set; }
}
