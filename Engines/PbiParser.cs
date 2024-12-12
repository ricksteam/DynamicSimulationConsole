using OsmSharp;
using OsmSharp.Streams;
using Shared.Models;
using Node = OsmSharp.Node;

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

    public static (Dictionary<long?, Node>, List<Edge>) LoadDataFromPBF(string filePath)
    {
        var nodes = new Dictionary<long?, Node>();
        var edges = new List<Edge>();

        using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
        {
            var source = new PBFOsmStreamSource(fileStream);

            foreach (var element in source)
            {
                switch (element.Type)
                {
                    case OsmGeoType.Node:
                    {
                        if (element is Node { Id: { } } node) nodes[node.Id] = node;
                        break;
                    }
                    case OsmGeoType.Way:
                    {
                        var way = element as Way;
                        for (int i = 0; i < way.Nodes.Length - 1; i++)
                        {
                            edges.Add(new Edge
                            {
                                StartNodeId = way.Nodes[i],
                                EndNodeId = way.Nodes[i + 1],
                                Weight = 1 // Example weight
                            });
                        }

                        break;
                    }
                }
            }
        }

        return (nodes, edges);
    }

}