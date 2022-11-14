namespace DynamicSimulationConsole.RoadGraph.Repositories;

public class InMemoryConvoyRepository : IConvoyRepository
{
    private readonly Dictionary<Guid, Convoy> _convoys;

    public InMemoryConvoyRepository()
    {
        _convoys = new Dictionary<Guid, Convoy>();
    }

    public bool ContainsId(Guid id) => _convoys.ContainsKey(id);

    public bool TryGetConvoyById(Guid id, out Convoy convoy)
    {
        return _convoys.TryGetValue(id, out convoy);
    }

    public Guid AddConvoy(Convoy convoy)
    {
        var id = GenerateUniqueId();
        convoy.id = id;
        _convoys.Add(id, convoy);
        return id;
    }

    public void Clear() => _convoys.Clear();
    
    public bool TryDeleteConvoyById(Guid id)
    {
        return _convoys.Remove(id);
    }

    public IEnumerable<Convoy> GetAllConvoys()
    {
        return _convoys.Values;
    }

    private Guid GenerateUniqueId()
    {
        var id = Guid.NewGuid();
        while (_convoys.ContainsKey(id))
        {
            id = Guid.NewGuid();
        }

        return id;
    }
}