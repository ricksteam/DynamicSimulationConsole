namespace DynamicSimulationConsole.RoadGraph;

public class Convoy
{
    public int maxSpeedMph;
    public double weightKg;
    
    public Convoy(int maxSpeedMph, double weightKg)
    {
        this.maxSpeedMph = maxSpeedMph;
        this.weightKg = weightKg;
    }
}