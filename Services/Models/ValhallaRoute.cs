using Shared.Models;

namespace DynamicSimulationConsole.Services.Models;

public class ValhallaRoute
{
    public ValhallaTrip trip { get; set; }
}

public class ValhallaTrip
{
    public LocationResponse[] locations { get; set; }
    public ValhallaLeg[] legs { get; set;  }
}

public class ValhallaLeg
{
    public string shape { get; set; }
}

public class ValhallaRouteNode
{
    public bool isBridge { get; set; }
    public LocationResponse location { get; set; }
}

public class ValhallaResponse
{
    public string shape { get; set; }
    public ValhallaRouteNode[] nodes { get; set; }
}
