using DynamicSimulationConsole.DataAccessLayer.Models;

namespace DynamicSimulationConsole.WebApi.Models;

public class ConvoyInput
{
    public string ConvoyName { get; set; }
}

public class ConvoyVehicleInput
{
    public Guid ConvoyId { get; set; }
    public string VehicleName  { get; set; }
    
    public int VehicleMaxSpeed  { get; set; }
    
    public float VehicleWeight  { get; set; }
}