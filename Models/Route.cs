using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSimulationConsole.Models
{
    [System.Serializable]
    public class Route
    {
        public Tuple<int, int> NodeConnection { get; set; }
    }
}
