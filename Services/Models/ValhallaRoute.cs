using Shared.Models;

namespace DynamicSimulationConsole.Services.Models;


public class ValhallaResponse
{
    public ValhallaTrip trip { get; set; }
    public ValhallaRoute[] alternates { get; set; }
}
public class ValhallaRoute
{
    public ValhallaTrip trip { get; set; }
    
    public ValhallaRoute(ValhallaTrip trip)
    {
        this.trip = trip;
    }
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
