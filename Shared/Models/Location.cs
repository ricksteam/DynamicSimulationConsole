using Newtonsoft.Json;

namespace Shared.Models;

public class Location
{
    public float lat { get; set; }
    public float lon { get; set; }
    public string? type { get; set; }
}

public class LocationResponse : Location
{
    [JsonProperty(PropertyName = "side_of_street")]
    public string sideOfStreet { get; set; }
}