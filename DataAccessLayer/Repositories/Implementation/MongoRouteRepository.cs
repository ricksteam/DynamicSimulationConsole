using DynamicSimulationConsole.DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DynamicSimulationConsole.DataAccessLayer.Repositories;

public class MongoRouteRepository : IRouteRepository
{
    private readonly IMongoCollection<Route> _routes;

    public MongoRouteRepository()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://admin:admin_password@cluster0.2pvyh5c.mongodb.net/?retryWrites=true&w=majority");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var db = client.GetDatabase("dynamic_simulation");
        _routes = db.GetCollection<Route>("routes");
    }
    
    public bool TryGetRouteById(Guid id, out Route route)
    {
        var filter_id = Builders<Route>.Filter.Eq("RouteId", id);
        var found = _routes.Find(filter_id).FirstOrDefault();
        route = found;
        return found != null;
    }

    public void AddRoute(Route convoy)
    {
        _routes.InsertOne(convoy);
    }

    // public void UpdateConvoy(Convoy newConvoy)
    // {
    //     var filter = Builders<Convoy>.Filter.Eq(s => s.ConvoyId, newConvoy.ConvoyId);
    //     //var update = Builders<Convoy>.Update.Set(s => s.VehicleCount, newConvoy.VehicleCount);
    //     //var result = _convoys.UpdateOne(filter, update);
    //     _convoys.ReplaceOne(filter, newConvoy, new ReplaceOptions() { IsUpsert = false });
    // }
    //
    // public async void AddRouteVehicle(Guid routeId, ConvoyVehicle vehicle)
    // {
    //     vehicle.VehicleId = new Guid();
    //     var filter = Builders<Convoy>.Filter.Eq(s => s.ConvoyId, convoyId);
    //     var update = Builders<Convoy>.Update.Push(s => s.Vehicles, vehicle);
    //     var result =  await _routes.UpdateOneAsync(filter, update);
    // }

    public bool TryDeleteRouteById(Guid id)
    {
        var filter_id = Builders<Route>.Filter.Eq("RouteId", id);
        var found = _routes.DeleteOne(filter_id).DeletedCount;
        return found > 0;
    }

    public IEnumerable<Route> GetAllRoutes()
    {
        return _routes.AsQueryable();
    }

}