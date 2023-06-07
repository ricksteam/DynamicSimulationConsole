using DynamicSimulationConsole.DataAccessLayer.Models;

namespace DynamicSimulationConsole.DataAccessLayer.Repositories;

public interface IRouteRepository
{
    public bool TryGetRouteById(Guid id, out Route convoy);
    public void AddRoute(Route convoy);
    public bool TryDeleteRouteById(Guid id);
    public IEnumerable<Route> GetAllRoutes();
}