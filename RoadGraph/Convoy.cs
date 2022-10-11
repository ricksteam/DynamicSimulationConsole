using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.RoadGraph;

public class Convoy
{
    public Guid id;

    public List<ConvoyVehicle> vehicles;
    
    public NodeCoordinate startPosition;
    public NodeCoordinate endPosition;
    public Convoy(NodeCoordinate startPosition, NodeCoordinate endPosition)
    {
        vehicles = new List<ConvoyVehicle>();
        this.startPosition = startPosition;
        this.endPosition = endPosition;
    }
}

public class ConvoyVehicle
{
    public Guid id;
    public int maxSpeedMph;   
    public float weightKg;

    public ConvoyVehicle(int maxSpeedMph, float weightKg)
    {
        this.maxSpeedMph = maxSpeedMph;
        this.weightKg = weightKg;

    }
}