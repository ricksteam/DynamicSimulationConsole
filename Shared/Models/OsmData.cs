namespace Shared.Models;

public class OsmData
{
    public List<OsmNode> Nodes { get; set; }
    public List<OsmBridge> Bridges { get; set; }
}

public class OsmNode
{
    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class OsmBridge
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<OsmNode> Nodes { get; set; }
    public Dictionary<string, string> Tags { get; set; }
}