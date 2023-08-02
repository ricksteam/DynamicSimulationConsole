using Itinero.Attributes;
using Itinero.Profiles;

namespace Engines.Profiles;

public class ConvoyProfile : Vehicle
{
    public ConvoyProfile()
    {
        Name = "convoy";
    }
    
    
    public override FactorAndSpeed FactorAndSpeed(IAttributeCollection attributes, Whitelist whitelist)
    {
        Console.WriteLine(attributes);
        foreach (var wl in whitelist)
        {
            Console.Write(wl);
            Console.Write(" | ");
        }
 
        Console.WriteLine();
        return new FactorAndSpeed()
        {
            SpeedFactor = 1,
        };
    }

    public override string Name { get; }
}