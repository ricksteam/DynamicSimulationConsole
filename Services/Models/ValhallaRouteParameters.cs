using Shared.Models;

namespace DynamicSimulationConsole.Services.Models;

public class ValhallaRouteParameters
{
    public Location[] locations { get; set; }
    public string costing { get; set; }
    public string units { get; set; }
}