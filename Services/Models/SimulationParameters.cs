using Shared.Models;

namespace DynamicSimulationConsole.Services.Models;

public class SimulationParameters
{
    public LatLng StartLocation { get; set; }
    public LatLng EndLocation { get; set; }
}