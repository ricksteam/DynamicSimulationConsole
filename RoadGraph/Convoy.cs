namespace DynamicSimulationConsole.RoadGraph;

public class Convoy
{
    public Guid id;

    public List<ConvoyVehicle> vehicles;
    public Convoy()
    {
        vehicles = new List<ConvoyVehicle>();
    }
}

public class ConvoyVehicle
{
    public int maxSpeedMph;
    public double weightKg;
    
    public ConvoyVehicle(int maxSpeedMph, double weightKg)
    {
        this.maxSpeedMph = maxSpeedMph;
        this.weightKg = weightKg;
    }
}