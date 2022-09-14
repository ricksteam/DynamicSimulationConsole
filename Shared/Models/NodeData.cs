namespace DynamicSimulationConsole.Shared.Models;

[Serializable]
public enum NodeDataType
{
    SpeedLimit,
    WeightLimit
}
[Serializable]
public class NodeData
{
    public NodeDataType nodeDataType { get; set; }
    public string nodeDataValue { get; set; }
}