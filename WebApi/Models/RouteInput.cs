using Shared.Models;

namespace DynamicSimulationConsole.WebApi.Models;

public class RouteInput
{
    public string RouteName { get; set; }
    public LatLng StartCoordinate { get; set; }
    public LatLng EndCoordinate { get; set; }
}