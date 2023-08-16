using DynamicSimulationConsole.Engines.Models;
using Engines.Interface;
using Engines.Profiles;
using Itinero;
using Itinero.Algorithms.Networks.Analytics.Trees;
using Itinero.Data.Network;
using Itinero.Geo;
using Itinero.IO.Osm;
using Itinero.LocalGeo;
using Itinero.Optimization;
using Itinero.Profiles;
using Shared.Models;
using Vehicle = Itinero.Osm.Vehicles.Vehicle;

namespace Engines;

public class ItineroEngine : ISimulationEngine
{
    private readonly RouterDb _routerDb;
    private readonly Router _router;

    public ItineroEngine()
    {
        var profileLuaFile = Directory.GetCurrentDirectory() + "\\..\\Engines\\Profiles\\convoy.lua";
        var profileCsFile = Directory.GetCurrentDirectory() + "\\..\\Engines\\Profiles\\ConvoyProfile.cs";
        using var profileStream = File.OpenRead(profileLuaFile);
        //var teee = new ConvoyProfile();
        var convoyProfile = DynamicVehicle.LoadFromStream(profileStream);
        
        _routerDb = new RouterDb();
        
        var file = Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-merge-v1-2.pbf";
        using var fileStream = System.IO.File.OpenRead(file);
        _routerDb.LoadOsmData(fileStream, convoyProfile);
        _router = new Router(_routerDb);
        Console.WriteLine("LOADING COMPLETE");
    }
    
    public RouteResult[] Test(LatLng startPoint, LatLng endPoint)
    {
        var profile = _routerDb.GetSupportedVehicle("convoy");
        var fastestRoute = GetRoute(profile.Fastest(), startPoint, endPoint);
        var shortestRoute = GetRoute(profile.Shortest(), startPoint, endPoint);
        foreach (var t in fastestRoute.Branches)
        {
            var found = t.Attributes.Where(x => x.Key == "bridge");
            foreach (var b in found)
            {
                Console.Write(b.ToString() + " | ");
            }
            Console.WriteLine();
        }
        return new RouteResult[]
        {
            new RouteResult(fastestRoute.Shape),
            new RouteResult(shortestRoute.Shape)
        };
    }

    private Route GetRoute(Profile profile, LatLng startPoint, LatLng endPoint)
    {
        var start = _router.Resolve(profile, (float)startPoint.lat, (float)startPoint.lon);
        var end = _router.Resolve(profile, (float)endPoint.lat, (float)endPoint.lon);

        var route = _router.Calculate(profile, start, end);
        return route;
    }
}