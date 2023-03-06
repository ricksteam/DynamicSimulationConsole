using DynamicSimulationConsole.DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DynamicSimulationConsole.DataAccessLayer.Repositories;

public class MongoConvoyRepository : IConvoyRepository
{
    private readonly IMongoCollection<Convoy> _convoys;

    public MongoConvoyRepository()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://admin:admin_password@cluster0.2pvyh5c.mongodb.net/?retryWrites=true&w=majority");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var db = client.GetDatabase("dynamic_simulation");
        _convoys = db.GetCollection<Convoy>("convoys");
    }
    
    public bool TryGetConvoyById(Guid id, out Convoy convoy)
    {
        var filter_id = Builders<Convoy>.Filter.Eq("ConvoyId", id);
        var found = _convoys.Find(filter_id).FirstOrDefault();
        convoy = found;
        return found != null;
    }

    public void AddConvoy(Convoy convoy)
    {
        _convoys.InsertOne(convoy);
    }

    public void UpdateConvoy(Convoy newConvoy)
    {
        var filter = Builders<Convoy>.Filter.Eq(s => s.ConvoyId, newConvoy.ConvoyId);
        //var update = Builders<Convoy>.Update.Set(s => s.VehicleCount, newConvoy.VehicleCount);
        //var result = _convoys.UpdateOne(filter, update);
        _convoys.ReplaceOne(filter, newConvoy, new ReplaceOptions() { IsUpsert = false });
    }
    
    public async void AddConvoyVehicle(Guid convoyId, ConvoyVehicle vehicle)
    {
        vehicle.VehicleId = new Guid();
        var filter = Builders<Convoy>.Filter.Eq(s => s.ConvoyId, convoyId);
        var update = Builders<Convoy>.Update.Push(s => s.Vehicles, vehicle);
        var result =  await _convoys.UpdateOneAsync(filter, update);
    }

    public bool TryDeleteConvoyById(Guid id)
    {
        var filter_id = Builders<Convoy>.Filter.Eq("ConvoyId", id);
        var found = _convoys.DeleteOne(filter_id).DeletedCount;
        return found > 0;
    }

    public IEnumerable<Convoy> GetAllConvoys()
    {
        return _convoys.AsQueryable();
    }

}