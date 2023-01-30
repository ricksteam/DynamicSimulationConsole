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

