using MongoDB.Bson;
using MongoDB.Driver;

namespace DynamicSimulationConsole.RoadGraph.Repositories;

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