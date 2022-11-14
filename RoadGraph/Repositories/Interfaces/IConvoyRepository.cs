namespace DynamicSimulationConsole.RoadGraph.Repositories;

public interface IConvoyRepository
{
    public bool ContainsId(Guid id);
    public bool TryGetConvoyById(Guid id, out Convoy convoy);
    public Guid AddConvoy(Convoy convoy);
    public void Clear();
    public bool TryDeleteConvoyById(Guid id);

    public IEnumerable<Convoy> GetAllConvoys();
}