namespace DynamicSimulationConsole.RoadGraph.Repositories;

public interface IRoadGraphRepository
{
    public bool ContainsId(Guid id);
    public bool TryGetGraphById(Guid id, out Graph graph);
    public Guid AddGraph(Graph graph);
    public void Clear();
    public bool TryDeleteGraphById(Guid id);
}