namespace DynamicSimulationConsole.RoadGraph.Repositories;

public interface IConvoyRepository
{
    public bool TryGetConvoyById(Guid id, out Convoy convoy);
    public void AddConvoy(Convoy convoy);
    public bool TryDeleteConvoyById(Guid id);
    public IEnumerable<Convoy> GetAllConvoys();
}