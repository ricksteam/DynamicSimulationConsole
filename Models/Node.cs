using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSimulationConsole.Models
{
    public enum NodeType
    {
        Road = 0,
        Bridge = 1
    }

    [System.Serializable]
    public class Node
    {
        public int NodeId { get; set; } // GUID
        public NodeType NodeType { get; set; }
        public NodeCoordinate Coordinate { get; set; }
    }
}
