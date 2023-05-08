using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Shared.Models;

namespace Engines;

public class OsmParser
{
    public static List<OsmNode> ExtractNodesAndBridges(string osmFilePath, out List<OsmBridge> bridges)
    {
        bridges = new List<OsmBridge>();
        var nodes = new List<OsmNode>();

        Dictionary<long, OsmNode> nodesLookup = new Dictionary<long, OsmNode>();

        using (XmlReader reader = XmlReader.Create(osmFilePath))
        {
            OsmBridge currentBridge = null;
            List<long> currentNodeRefs = null;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "node")
                    {
                        long id = long.Parse(reader.GetAttribute("id"));
                        double lat = double.Parse(reader.GetAttribute("lat"), CultureInfo.InvariantCulture);
                        double lon = double.Parse(reader.GetAttribute("lon"), CultureInfo.InvariantCulture);

                        var node = new OsmNode { Id = id, Latitude = lat, Longitude = lon };
                        nodesLookup[id] = node;
                        nodes.Add(node);
                    }
                    else if (reader.Name == "way")
                    {
                        currentBridge = new OsmBridge { Id = long.Parse(reader.GetAttribute("id")) };
                        currentNodeRefs = new List<long>();
                    }
                    else if (reader.Name == "nd" && currentBridge != null)
                    {
                        currentNodeRefs.Add(long.Parse(reader.GetAttribute("ref")));
                    }
                    else if (reader.Name == "tag" && currentBridge != null)
                    {
                        string key = reader.GetAttribute("k");
                        string value = reader.GetAttribute("v");

                        if (key == "bridge" && value == "yes")
                        {
                            currentBridge.Nodes = currentNodeRefs.Select(id => nodesLookup[id]).ToList();
                            bridges.Add(currentBridge);
                        }
                        else if (key == "name")
                        {
                            currentBridge.Name = value;
                        }
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "way")
                {
                    currentBridge = null;
                    currentNodeRefs = null;
                }
            }
        }

        return nodes;
    }

    public static List<OsmNode> ExtractNodes(XDocument osmDocument)
    {
        Console.WriteLine("Parsing Nodes...");
        List<OsmNode> nodes = new List<OsmNode>();

        foreach (XElement node in osmDocument.Descendants("node"))
        {
            nodes.Add(new OsmNode
            {
                Id = long.Parse(node.Attribute("id").Value),
                Latitude = double.Parse(node.Attribute("lat").Value, CultureInfo.InvariantCulture),
                Longitude = double.Parse(node.Attribute("lon").Value, CultureInfo.InvariantCulture)
            });
        }
        Console.WriteLine("Completed Parsing Nodes!");
        return nodes;
    }
    
    public static List<OsmBridge> ExtractBridges(XDocument osmDocument, List<OsmNode> nodes)
    {
        Console.WriteLine("Parsing Bridges...");
        var bridgeWays = osmDocument.Descendants("way")
            .Where(w => w.Elements("tag").Any(t => t.Attribute("k").Value == "bridge" && t.Attribute("v").Value == "yes"))
            .ToList();

        var bridges = new List<OsmBridge>();

        foreach (var bridgeWay in bridgeWays)
        {
            var nodeRefs = bridgeWay.Elements("nd")
                .Select(nd => long.Parse(nd.Attribute("ref").Value))
                .ToList();

            var bridgeNodes = nodes.Where(n => nodeRefs.Contains(n.Id)).ToList();

            var nameTag = bridgeWay.Elements("tag").FirstOrDefault(t => t.Attribute("k").Value == "name");
            string bridgeName = nameTag?.Attribute("v").Value ?? "Unnamed Bridge";

            bridges.Add(new OsmBridge
            {
                Id = long.Parse(bridgeWay.Attribute("id").Value),
                Name = bridgeName,
                Nodes = bridgeNodes
            });
        }
        Console.WriteLine("Completed Parsing Bridges!");
        return bridges;
    }
}