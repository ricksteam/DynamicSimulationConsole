namespace DynamicSimulationConsole.WebApi.Models;

public class ConvoyInput
{
    public int numberOfVehicles { get; set; } 
    public int maxSpeedMph { get; set; }
    public double weightKg { get; set; }
}