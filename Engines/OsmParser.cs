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
        var nodesLookup = new Dictionary<long, OsmNode>();

        using var reader = XmlReader.Create(osmFilePath);

        OsmBridge currentBridge = null;
        List<long> currentNodeRefs = null;

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "node":
                        ProcessNode(reader, nodes, nodesLookup);
                        break;
                    case "way":
                        currentBridge = ProcessWay(reader);
                        currentNodeRefs = new List<long>();
                        break;
                    case "nd":
                        ProcessNd(reader, currentBridge, currentNodeRefs);
                        break;
                    case "tag":
                        ProcessTag(reader, currentBridge, bridges, nodesLookup, currentNodeRefs);
                        break;
                }
            }
            else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "way")
            {
                currentBridge = null;
                currentNodeRefs = null;
            }
        }

        return nodes;
    }

    private static void ProcessNode(XmlReader reader, List<OsmNode> nodes, Dictionary<long, OsmNode> nodesLookup)
    {
        var id = long.Parse(reader.GetAttribute("id") ?? string.Empty);
        var lat = ParseCoordinate(reader.GetAttribute("lat"));
        var lon = ParseCoordinate(reader.GetAttribute("lon"));

        var node = new OsmNode { Id = id, Latitude = lat, Longitude = lon };
        nodesLookup[id] = node;
        nodes.Add(node);
    }

    private static OsmBridge ProcessWay(XmlReader reader)
    {
        return new OsmBridge { Id = long.Parse(reader.GetAttribute("id") ?? string.Empty) };
    }

    private static void ProcessNd(XmlReader reader, OsmBridge currentBridge, List<long> currentNodeRefs)
    {
        if (currentBridge == null) return;
        currentNodeRefs.Add(long.Parse(reader.GetAttribute("ref") ?? string.Empty));
    }

    private static void ProcessTag(XmlReader reader, OsmBridge currentBridge, ICollection<OsmBridge> bridges,
        IReadOnlyDictionary<long, OsmNode> nodesLookup, IEnumerable<long> currentNodeRefs)
    {
        if (currentBridge == null) return;

        var key = reader.GetAttribute("k");
        var value = reader.GetAttribute("v");

        if (key == null || value == null) return;

        if (key == "bridge" && value == "yes")
        {
            currentBridge.Nodes = currentNodeRefs.Select(id => nodesLookup[id]).ToList();
            bridges.Add(currentBridge);
        }
        else if (key == "name")
        {
            currentBridge.Name = value;
        }
        else if (key == "nbi")
        {
            currentBridge.Nbi = value;
        }
        else if (currentBridge?.Nbi == "yes")
        {
            if (value == "N")
                value = "0";
            
            switch (key)
            {
                case "nbi:super-cond":
                    currentBridge.SuperCondition = double.Parse(value);
                    break;
                case "nbi:sub-cond":
                    currentBridge.SubCondition = double.Parse(value);
                    break;
                case "nbi:op-rating":
                    currentBridge.OperationRating = double.Parse(value);
                    break;
                case "nbi:op-method-code":
                    currentBridge.OperationMethodCode = int.Parse(value);
                    break;
                case "nbi:deck-rating":
                    currentBridge.DeckRating = double.Parse(value);
                    break;
                case "nbi:inv-rating":
                    currentBridge.InventoryRating = double.Parse(value);
                    break;
            }
        }
    }

    private static double ParseCoordinate(string coordinate)
    {
        return double.Parse(coordinate, CultureInfo.InvariantCulture);
    }
}