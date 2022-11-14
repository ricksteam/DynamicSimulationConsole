using DynamicSimulationConsole.Shared.Models;

namespace DynamicSimulationConsole.RoadGraph;

public class Convoy
{
    public Guid id;
    public string convoyName;

    public List<ConvoyVehicle> vehicles;
    public Convoy(string convoyName, ConvoyVehicle[] vehicles)
    {
        this.convoyName = convoyName;
        this.vehicles = vehicles.ToList();
    }
}

public class ConvoyVehicle
{
    public int VehicleId { get; set; }
    public string VehicleName  { get; set; }
    public int VehicleMaxSpeed  { get; set; }
    public float VehicleWeight  { get; set; }
}