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
    public string Nbi { get; set; }
    public List<OsmNode> Nodes { get; set; }
    public double SuperCondition { get; set; }
    public double SubCondition { get; set; }
    public double DeckRating { get; set; }
    public double InventoryRating { get; set; }
    public double OperationRating { get; set; }
    public int OperationMethodCode { get; set; }
}