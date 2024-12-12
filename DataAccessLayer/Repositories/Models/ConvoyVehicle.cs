using MongoDB.Bson.Serialization.Attributes;

namespace DynamicSimulationConsole.DataAccessLayer.Models;

[BsonIgnoreExtraElements]
public class ConvoyVehicle
{
    [BsonId]
    public Guid VehicleId { get; set; }

    [BsonElement("VehicleName")]
    public string VehicleName  { get; set; }
    
    [BsonElement("VehicleMaxSpeed")]
    public int VehicleMaxSpeed  { get; set; }
    
    [BsonElement("VehicleWeight")]
    public float VehicleWeight  { get; set; }

    public ConvoyVehicle(string vehicleName, int vehicleMaxSpeed, float vehicleWeight)
    {
        VehicleName = vehicleName;
        VehicleWeight = vehicleWeight;
        VehicleMaxSpeed = vehicleMaxSpeed;
    }
}