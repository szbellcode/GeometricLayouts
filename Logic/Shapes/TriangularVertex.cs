using System;
using System.Collections.Generic;

namespace Logic.Shapes
{
    /// <summary>
    /// The set of 3 coordinates which represent each point in a triangle.
    /// </summary>
    public class TriangularVertex : IEquatable<TriangularVertex>
    {
        public TriangularVertex(Coordinates vertex1, Coordinates vertex2, Coordinates vertex3)
        {
            Vertex1 = vertex1 ?? throw new ArgumentException(nameof(vertex1));
            Vertex2 = vertex2 ?? throw new ArgumentException(nameof(vertex2));
            Vertex3 = vertex3 ?? throw new ArgumentException(nameof(vertex3));
        }

        public Coordinates Vertex1 { get; }
        public Coordinates Vertex2 { get; }
        public Coordinates Vertex3 { get; }

        public override string ToString()
        {
            return $"V1: {Vertex1} - V2:{Vertex2} - V3:{ Vertex3}";
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return Equals(obj as TriangularVertex);
        }

        public bool Equals(TriangularVertex other)
        {
            return other != null &&
                   EqualityComparer<Coordinates>.Default.Equals(Vertex1, other.Vertex1) &&
                   EqualityComparer<Coordinates>.Default.Equals(Vertex2, other.Vertex2) &&
                   EqualityComparer<Coordinates>.Default.Equals(Vertex3, other.Vertex3);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vertex1, Vertex2, Vertex3);
        }

        public static bool operator ==(TriangularVertex left, TriangularVertex right)
        {
            return EqualityComparer<TriangularVertex>.Default.Equals(left, right);
        }

        public static bool operator !=(TriangularVertex left, TriangularVertex right)
        {
            return !(left == right);
        }
        #endregion
    }
}
