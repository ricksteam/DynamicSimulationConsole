﻿
namespace DynamicSimulationConsole.Shared.Models
{
    public enum NodeType
    {
        Road = 0,
        Bridge = 1
    }

    [Serializable]
    public class Node
    {
        public int nodeId { get; set; } // GUID
        public NodeType nodeType { get; set; }
        public NodeCoordinate coordinate { get; set; }
        
        public bool oneWay { get; set; }
    }
}