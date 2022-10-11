using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.RoadGraph;

public class Convoy
{
    public Guid id;

    public List<ConvoyVehicle> vehicles;
    
    public int startPositionId;
    public int endPositionId;
    public Convoy(int startPositionId, int endPositionId, ConvoyVehicle[] vehicles)
    {
        this.vehicles = vehicles.ToList();
        this.startPositionId = startPositionId;
        this.endPositionId = endPositionId;
    }
}

public class ConvoyVehicle
{
    public int VehicleId { get; set; }
    public string VehicleName  { get; set; }
    public int VehicleMaxSpeed  { get; set; }
    public float VehicleWeight  { get; set; }
}