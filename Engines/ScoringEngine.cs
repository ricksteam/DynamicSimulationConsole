using DynamicSimulationConsole.Engines.Models;
using Engines.Interface;
using GeoAPI.Geometries;
using Newtonsoft.Json;
using Npgsql;
using Shared.Models;

namespace Engines;

public class ScoringEngine : ISimulationEngine
{
    public RouteResult[] Test(LatLng startPoint, LatLng endPoint, int numberOfRoutes)
    {
        List<RouteResult> results = new List<RouteResult>();
        
        const string connString = "Host=localhost;Username=postgres;Password=1551;Database=postgres";

        try
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            //using var cmd = new NpgsqlCommand("SELECT * FROM get_routes(@start_lat, @start_lon, @end_lat, @end_lon, @num_routes)", conn);
            using var cmd = new NpgsqlCommand("SELECT * FROM get_routes_json(@start_lat, @start_lon, @end_lat, @end_lon, @num_routes)", conn);

            cmd.Parameters.AddWithValue("start_lat",  startPoint.lat); 
            cmd.Parameters.AddWithValue("start_lon", startPoint.lon); 
            cmd.Parameters.AddWithValue("end_lat", endPoint.lat); 
            cmd.Parameters.AddWithValue("end_lon", endPoint.lon); 
            cmd.Parameters.AddWithValue("num_routes", numberOfRoutes);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // var seq = reader.GetInt32(reader.GetOrdinal("seq"));
                // var path_id = reader.GetInt32(reader.GetOrdinal("path_id"));
                // var node = reader.GetInt64(reader.GetOrdinal("node"));
                // var edge = reader.GetInt64(reader.GetOrdinal("edge"));
                // var cost = reader.GetDouble(reader.GetOrdinal("cost"));
                // var longitude = reader.GetDouble(reader.GetOrdinal("longitude"));
                // var latitude = reader.GetDouble(reader.GetOrdinal("latitude"));
                //
                // Console.WriteLine($"Seq: {seq}, Path ID: {path_id}, Node: {node}, Edge: {edge}, Cost: {cost}, Longitude: {longitude}, Latitude: {latitude}");
                
                //int pathId = reader.GetInt32(0);
                string routeDetailsJson = reader.GetString(0);

                // Deserialize the JSON into your route and nodes objects
                // Assuming you have classes defined for your routes and nodes
                var routeDetails = JsonConvert.DeserializeObject<Node[]>(routeDetailsJson);
                var routeResult = new RouteResult(routeDetails);
                results.Add(routeResult);
            }

            //Console.WriteLine($"Stored procedure returned {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        
        
        // using (var cmd = new NpgsqlCommand(sql, conn))
        // {
        //     using (var reader = cmd.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             Console.WriteLine(
        //                 $"Seq: {reader.GetInt32(0)}, Node: {reader.GetInt32(1)}, Edge: {reader.GetInt32(2)}, Cost: {reader.GetDouble(3)}");
        //         }
        //     }
        // }
        //
        // conn.Close();
        return results.ToArray();
    }

    // const double LENGTH_WEIGHT = 0.5;
    // const double BRIDGE_CONDITION_WEIGHT = 1.0;
    //
    // public void CalculateScore(IEnumerable<OsmNode> route, object convoy)
    // {
    //     var totalLength = 0.0;
    //     var totalBridgeCondition = 0.0;
    //
    //     foreach (OsmNode segment in route)
    //     {
    //         totalLength += segment.Length;
    //         
    //         if (segment.Bridge != null)
    //         {
    //             if (convoy.Weight > segment.Bridge.Capacity) 
    //                 return double.MaxValue;
    //
    //             totalBridgeCondition += segment.Bridge.ConditionScore;
    //         }
    //     }
    //
    //     return LENGTH_WEIGHT * totalLength + BRIDGE_CONDITION_WEIGHT * totalBridgeCondition;
    // }
    //
    // private double CalculateStrategicImportance(IEnumerable<OsmNode> route)
    // {
    //     
    // }
    // private double CalculateAlternateOptions(IEnumerable<OsmNode> route)
    // {
    //     
    // }
    // private double CalculateConvoySuitability(IEnumerable<OsmNode> route, object convoy)
    // {
    //     
    // }
    
}