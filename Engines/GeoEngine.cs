using GeoCoordinatePortable;
using Shared.Models;

namespace Engines;

public abstract class GeoEngine
{
    public static double CalculateMinimumDistance(LatLng point1, LatLng point2, OsmNode node)
    {
        var p1 = new GeoCoordinate(point1.lat, point1.lon);
        var p2 = new GeoCoordinate(point2.lat, point2.lon);
        var p3 = new GeoCoordinate(node.Latitude, node.Longitude);

        var s = p1.GetDistanceTo(p2);
        var r = ((p1.Latitude - p3.Latitude) * (p1.Latitude - p2.Latitude) +
                 (p1.Longitude - p3.Longitude) * (p1.Longitude - p2.Longitude)) / (s * s);

        if (r is >= 0 and <= 1)
        {
            var s1 = p1.GetDistanceTo(p3);
            var d = Math.Sqrt(Math.Pow(s1, 2) - Math.Pow(r * s, 2));
            return d;
        }

        var d1 = p1.GetDistanceTo(p3);
        var d2 = p2.GetDistanceTo(p3);
        return Math.Min(d1, d2);
    }
}