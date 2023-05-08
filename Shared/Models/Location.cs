using Newtonsoft.Json;

namespace Shared.Models;

public class LatLng
{
    public double lat { get; set; }
    public double lon { get; set; }
}

public class Location : LatLng
{
    public string? type { get; set; }
}

public class LocationResponse : Location
{
    [JsonProperty(PropertyName = "side_of_street")]
    public string sideOfStreet { get; set; }
}