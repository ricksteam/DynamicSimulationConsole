using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSimulationConsole.Models
{
    public enum UnitOfLength
    {
        Miles,
        Kilometers
    }

    [System.Serializable]
    public struct NodeCoordinate
    {

        public double Lat { get; set; }
        public double Lon { get; set; }

        public NodeCoordinate() : this(0, 0) { }
        public NodeCoordinate(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public static bool operator ==(NodeCoordinate a, NodeCoordinate b)
        {
            return a.Lat == b.Lat && a.Lon == b.Lon;
        }

        public static bool operator !=(NodeCoordinate a, NodeCoordinate b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return this == (NodeCoordinate)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Lat, Lon);
        }

        public override string ToString()
        {
            return $"Latitude: {Lat} | Longitude: {Lon}";
        }

    }

    public static class NodeCoordinateExtensions
    {
        private const double MILES_TO_KILOMETERS_FACTOR = 1.609344;
        public static double DistanceTo(this NodeCoordinate from, NodeCoordinate to, UnitOfLength uol = UnitOfLength.Kilometers) => GetDistance(from, to, uol);
        public static double DistanceFrom(this NodeCoordinate to, NodeCoordinate from, UnitOfLength uol = UnitOfLength.Kilometers) => GetDistance(to, from, uol);


        private static double GetDistance(NodeCoordinate a, NodeCoordinate b, UnitOfLength uol)
        {
            var rLat1 = Math.PI * a.Lat / 180;
            var rLat2 = Math.PI * b.Lat / 180;

            var theta = a.Lon - b.Lon;
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
