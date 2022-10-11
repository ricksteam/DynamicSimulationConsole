
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
        
        public List<NodeData> nodeData { get; set; }
        public NodeCoordinate coordinate { get; set; }
        
        public bool oneWay { get; set; }

        // public T GetNodeDataByType<T>(NodeDataType type)
        // {
        //     var data = nodeData.Find(nd => nd.nodeDataType == type);
        //     if (data != null)
        //     {
        //         if (typeof(T) == typeof(int))
        //         {
        //             if (float.TryParse(data.nodeDataValue, out var floatValue))
        //             {
        //                 return (float)floatValue; 
        //             }
        //
        //             return default(T);
        //         }
        //         
        //     }
        //
        //     return 0;
        // }

        private NodeData GetNodeDataByType(NodeDataType type)
        {
            var result = nodeData.Find(nd => nd.nodeDataType == type);
            return result;
        } 

        public double GetSpeedLimitMph()
        {
            var data = GetNodeDataByType(NodeDataType.SpeedLimit);
            //return float.TryParse(data.nodeDataValue, out var value) ? value : default(float);
            return data?.nodeDataValue ?? default;
        }

        public double GetWeightLimitKg()
        {
            var data = GetNodeDataByType(NodeDataType.WeightLimit);
            //return float.TryParse(data.nodeDataValue, out var value) ? value : default(float);
            return data?.nodeDataValue ?? default;
        }
    }
}
