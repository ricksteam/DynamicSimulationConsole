

namespace DynamicSimulationConsole
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var simulation = new Simulation();
            var socketThread = new Thread(() =>
            {
                var network = new NetworkController();
                network.StartListening();
            });
            socketThread.Start();
        }
    }
}