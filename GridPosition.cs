using System.Numerics;

namespace DynamicSimulationConsole
{

    [System.Serializable]
    public struct GridPosition
    {
        public int X { get; }
        public int Z { get; }

        public GridPosition(int x, int z)
        {
            X = x;
            Z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, 0, Z);
        }

        public override string ToString()
        {
            return $"GridPosition: {X}, {Z}";
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return (a.X == b.X && a.Z == b.Z);
        }
        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return !(a == b);
        }

        public static GridPosition operator *(GridPosition a, int b)
        {
            return new GridPosition(a.X * b, a.Z * b);
        }
        public override bool Equals(object obj)
        {
            return obj is GridPosition position && X == position.X && Z == position.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Z);
        }

        public bool Equals(GridPosition other)
        {
            return this == other;
        }

    }
}
