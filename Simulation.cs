using DynamicSimulationConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DynamicSimulationConsole
{
    public class Simulation
    {
        private Graph _network;
        public Simulation()
        {
            string text = System.IO.File.ReadAllText(@"D:\Graduate Research\DynamicSimulationConsole\TestInput.json");
            var input = JsonSerializer.Deserialize<SimulationInput>(text);
            Console.WriteLine(text);
            _network = new Graph(input);

            
        }
    }
}
