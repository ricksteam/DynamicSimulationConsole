using System.Diagnostics;
using DynamicSimulationConsole.Engines.Models;
using Engines.Interface;
using Newtonsoft.Json.Linq;
using Shared.Models;

namespace Engines.Implementation;

public class SimulationEngine : ISimulationEngine
{
    private static void ExecutePythonScript(LatLng startPoint, LatLng endPoint, int numberOfRoutes)
    {
        const string pythonPath = @"C:\Users\Cyber\miniconda3\envs\ox\python.exe";
        const string scriptPath = @"D:\Graduate Research\DynamicSimulationEngine\main.py";

        var arg1 = startPoint.lat;
        var arg2 = startPoint.lon;
        var arg3 = endPoint.lat;
        var arg4 = startPoint.lon; 
 
        var start = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"{scriptPath} {arg1} {arg2} {arg3} {arg4} {numberOfRoutes}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = false
        };
        
        using var process = Process.Start(start);
        using var reader = process.StandardOutput;
        
        var result = reader.ReadToEnd();
        //var jsonResult = JObject.Parse(result);
        Console.WriteLine(result);
    }
    
    public RouteResult[] Test(LatLng startPoint, LatLng endPoint, int numberOfRoutes)
    {
        ExecutePythonScript(startPoint, endPoint, 1);
        return Array.Empty<RouteResult>();
    }
}