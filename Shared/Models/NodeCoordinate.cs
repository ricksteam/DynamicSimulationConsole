
namespace DynamicSimulationConsole.Shared.Models
{
    public enum UnitOfLength
    {
        Miles,
        Kilometers
    }
    
    [Serializable]
    public struct NodeCoordinate
    {
        private const double EPSILON = 0.00001;
        public double latitude { get; set; }
        public double longitude { get; set; }

        public NodeCoordinate() : this(0, 0) { }
        public NodeCoordinate(double lat, double lon)
        {
            latitude = lat;
            longitude = lon;
        }

        public static bool operator ==(NodeCoordinate a, NodeCoordinate b)
        {
            return Math.Abs(a.latitude - b.latitude) < EPSILON && Math.Abs(a.longitude - b.longitude) < EPSILON;
        }

        public static bool operator !=(NodeCoordinate a, NodeCoordinate b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj != null && this == (NodeCoordinate)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(latitude, longitude);
        }

        public override string ToString()
        {
            return $"Latitude: {latitude} | Longitude: {longitude}";
        }

    }

    public static class NodeCoordinateExtensions
    {
        private const double MILES_TO_KILOMETERS_FACTOR = 1.609344;
        public static double DistanceTo(this NodeCoordinate from, NodeCoordinate to, UnitOfLength uol = UnitOfLength.Kilometers) => GetDistance(from, to, uol);
        public static double DistanceFrom(this NodeCoordinate to, NodeCoordinate from, UnitOfLength uol = UnitOfLength.Kilometers) => GetDistance(to, from, uol);

        private static double GetDistance(NodeCoordinate a, NodeCoordinate b, UnitOfLength uol)
        {
            var rLat1 = Math.PI * a.latitude / 180;
            var rLat2 = Math.PI * b.latitude / 180;

            var theta = a.longitude - b.longitude;
            var rTheta = Math.PI * theta / 180;

            var dist = Math.Sin(rLat1) * Math.Sin(rLat2) + Math.Cos(rLat1) * Math.Cos(rLat2) * Math.Cos(rTheta);

            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return uol switch
            {
                UnitOfLength.Miles => dist,
                UnitOfLength.Kilometers => dist * MILES_TO_KILOMETERS_FACTOR,
                _ => dist
            };
        }
    }
}
