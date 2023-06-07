using OsmSharp;
using OsmSharp.Streams;
using Shared.Models;

namespace Engines;

public class PbiParser
{
    public static List<OsmBridge> GetBridges(string pbiFilePath)
    {
        var bridgeList = new List<OsmBridge>();
        using var fileStream = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-merge-v1-1-1.pbf");
        using var source = new PBFOsmStreamSource(fileStream);
        foreach (var osmGeo in source)
        {
            if (osmGeo.Type == OsmGeoType.Way)
            {
                var node = osmGeo as Way;
                if (node == null) continue;
                Console.WriteLine($"WAY: {node.Id}");
                if (!node.Tags.Contains("bridge", "yes")) continue;
                
            }
        }

        return bridgeList;
    }
}