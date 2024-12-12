namespace Shared.Models;

public class Edge
{
    public long StartNodeId { get; set; }
    public long EndNodeId { get; set; }
    public double Weight { get; set; } // e.g., length of the road
}