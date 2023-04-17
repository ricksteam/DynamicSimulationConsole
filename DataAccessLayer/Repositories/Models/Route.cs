using MongoDB.Bson.Serialization.Attributes;

namespace DynamicSimulationConsole.DataAccessLayer.Models;

[BsonIgnoreExtraElements]
public class Route
{
    [BsonId]
    public Guid RouteId { get; set; }
    
    [BsonElement("RouteName")]
    public string RouteName { get; set; }
    
    public Route(string routeName)
    {
        RouteName = routeName;
    }
}