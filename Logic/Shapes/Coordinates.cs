using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Shapes
{
    /// <summary>
    /// Coordinates on the {x} and {y} plane.
    /// </summary>
    public class Coordinates : IEquatable<Coordinates>
    {
        private Coordinates()
        {
            // for json deserialisation
        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinates(string coordinates)
        {
            if (String.IsNullOrWhiteSpace(coordinates)) { throw new ArgumentException(nameof(coordinates)); }

            int[] coords = new List<int>(coordinates
                .Split(',')
                .Where(x => int.TryParse(x, out var result))
                .Select(int.Parse))
                .ToArray();

            if (coords.Length != 2) {
                throw new ArgumentException("Invalid coordinates format");
            }

            X = coords[0];
            Y = coords[1];
        }

        /// <summary>
        /// {x} plane coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// {y} plane coordinate
        /// </summary>
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinates);
        }

        public bool Equals(Coordinates other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return EqualityComparer<Coordinates>.Default.Equals(left, right);
        }

        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !(left == right);
        }
        #endregion
    }
}
