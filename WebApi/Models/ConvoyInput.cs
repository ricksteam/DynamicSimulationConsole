using DynamicSimulationConsole.DataAccessLayer.Models;

namespace DynamicSimulationConsole.WebApi.Models;

public class ConvoyInput
{
    public string ConvoyName { get; set; }
    public ConvoyVehicle[] Vehicles { get; set; }
}
