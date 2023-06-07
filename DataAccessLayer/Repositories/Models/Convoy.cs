using MongoDB.Bson.Serialization.Attributes;

namespace DynamicSimulationConsole.DataAccessLayer.Models;

[BsonIgnoreExtraElements]
public class Convoy
{
    [BsonId]
    public Guid ConvoyId { get; set; }
    
    [BsonElement("ConvoyName")]
    public string ConvoyName { get; set; }
    
    [BsonElement("Vehicles")]
    public List<ConvoyVehicle> Vehicles { get; set; }
    public Convoy(string convoyName)
    {
        ConvoyName = convoyName;
        Vehicles = new List<ConvoyVehicle>(); 
    }
}