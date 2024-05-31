using System;

namespace DannyG
{
    public struct Coordinate
    {
        public int x;
        public int y;
        public Coordinate(int x = -1, int y = -1)
        {
            this.x = x;
            this.y = y;
        }
        public Coordinate(Coordinate coordinate)
        {
            this.x = coordinate.x;
            this.y = coordinate.y;
        }

        #region Operations

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.x + b.x, a.y + b.y);
        }
        public static Coordinate operator -(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.x - b.x, a.y - b.y);
        }
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return a.x != b.x || a.y != b.y;
        }
        public bool Equals(Coordinate other)
        {
            return x == other.x && y == other.y;
        }
        public override bool Equals(object obj)
        {
            return obj is Coordinate other && Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        #endregion
        
        public void Update(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }
        public void Update(Coordinate coordinate)
        {
            this.x = coordinate.x;
            this.y = coordinate.y;
        }

        public void Increment(Incrementor2D incrementor)
        {
            x += incrementor.x;
            y += incrementor.y;
        }

        public override string ToString()
        {
            return $"{x}, {y}";
        }
    }
    public static class CoordinateExtension
    {
        public static bool IsNull(this Coordinate coordinate)
        {
            return coordinate is { x: -1, y: -1 };
        }
        public static void ToNull(this Coordinate coordinate)
        {
            coordinate.x = -1;
            coordinate.y = -1;
        }

        public static Coordinate WithUpdatedValues(this Coordinate coordinate, int x = 0, int y = 0)
        {
            Coordinate result = new Coordinate();
            result.Update(coordinate.x + x, coordinate.y + y);
            return result;
            
        }
    }
}