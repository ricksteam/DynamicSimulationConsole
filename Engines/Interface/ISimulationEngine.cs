using DynamicSimulationConsole.Engines.Models;
using Itinero;
using Itinero.LocalGeo;
using Shared.Models;

namespace Engines.Interface;

public interface ISimulationEngine
{
    public RouteResult[] Test(LatLng startPoint, LatLng endPoint, int numberOfRoutes);
}