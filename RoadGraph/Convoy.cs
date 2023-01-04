using MongoDB.Bson.Serialization.Attributes;

namespace DynamicSimulationConsole.RoadGraph;

[BsonIgnoreExtraElements]
public class Convoy
{
    [BsonId]
    public Guid ConvoyId { get; set; }
    
    [BsonElement("ConvoyName")]
    public string ConvoyName { get; set; }
    
    [BsonElement("Vehicles")]
    public List<ConvoyVehicle> Vehicles { get; set; }
    public Convoy(string convoyName, ConvoyVehicle[] vehicles)
    {
        ConvoyName = convoyName;
        Vehicles = vehicles.ToList();
    }
}

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
}