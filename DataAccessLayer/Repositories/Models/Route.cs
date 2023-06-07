using MongoDB.Bson.Serialization.Attributes;
using Shared.Models;

namespace DynamicSimulationConsole.DataAccessLayer.Models;

[BsonIgnoreExtraElements]
public class Route
{
    [BsonId]
    public Guid RouteId { get; set; }
    
    [BsonElement("RouteName")]
    public string RouteName { get; set; }
    
    [BsonElement("StartCoordinate")]
    public LatLng StartCoordinate { get; set; }
    
    [BsonElement("EndCoordinate")]
    public LatLng EndCoordinate { get; set; }
    
    public Route(string routeName, LatLng startCoordinate, LatLng endCoordinate)
    {
        RouteName = routeName;
        StartCoordinate = startCoordinate;
        EndCoordinate = endCoordinate;
    }
}