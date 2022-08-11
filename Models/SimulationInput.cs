using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSimulationConsole.Models
{
    [System.Serializable]
    public class SimulationInput
    {
        public Node[] Nodes;  
        public Tuple<int, int>[] Routes;
    }
}
