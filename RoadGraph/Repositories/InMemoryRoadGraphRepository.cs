namespace DynamicSimulationConsole.RoadGraph.Repositories;

public class InMemoryRoadGraphRepository : IRoadGraphRepository
{
    private readonly Dictionary<Guid, Graph> _graphs;

    public InMemoryRoadGraphRepository()
    {
        _graphs = new Dictionary<Guid, Graph>();
    }

    public bool ContainsId(Guid id) => _graphs.ContainsKey(id);

    public bool TryGetGraphById(Guid id, out Graph graph)
    {
        return _graphs.TryGetValue(id, out graph);
    }

    public Guid AddGraph(Graph graph)
    {
        var id = GenerateUniqueId();
        _graphs.Add(id, graph);
        return id;
    }

    public void Clear() => _graphs.Clear();
    
    public bool TryDeleteGraphById(Guid id)
    {
        return _graphs.Remove(id);
    }

    private Guid GenerateUniqueId()
    {
        var id = Guid.NewGuid();
        while (_graphs.ContainsKey(id))
        {
            id = Guid.NewGuid();
        }

        return id;
    }
}