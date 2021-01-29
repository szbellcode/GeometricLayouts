using Logic.Configuration;
using Logic.Shapes.Naming;
using System;

namespace Logic.Shapes
{
    /// <summary>
    /// A Right Angled Triangle which exists within a grid cell in either the top or bottom position.
    /// </summary>
    public class Triangle : ITriangle
    {
        public Triangle(ITriangleNameGenerator triangleNameGenerator, AppConfig appConfig, IGridCell parentGridCell, bool isBottom)
        {
            _triangleNameGenerator = triangleNameGenerator ?? throw new ArgumentNullException(nameof(triangleNameGenerator));
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig)); 
            ParentCell = parentGridCell ?? throw new ArgumentNullException(nameof(parentGridCell)); 
            IsBottom = isBottom;

            CalculateName();
        }

        private readonly ITriangleNameGenerator _triangleNameGenerator;
        private readonly AppConfig _appConfig;
        private TriangularVertex _vertices = null;
        private int NonHypotenuseSideLength { get { return _appConfig.NonHypotenuseSideLength; } }

        /// <summary>
        /// Name of the triangle in {X}{Y} format where {X} is an alphanumeric relating to the row 
        /// and {Y} is a number relating to the column in which the triangle resides e.g. 'B1'
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Indicates whether this triangle is on the bottom or top in its parent cell.
        /// </summary>
        public bool IsBottom { get; }

        /// <summary>
        /// Parent cell to which this triangle belong.
        /// </summary>
        public IGridCell ParentCell { get; }
            
        /// <summary>
        /// The coordinates for the triangle within the parent grid.
        /// </summary>
        public TriangularVertex Vertices
        {
            get
            {
                if (_vertices == null)
                {
                    _vertices = new TriangularVertex(GetCoordinates(1), GetCoordinates(2), GetCoordinates(3));
                }

                return _vertices;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} | Position:" + (IsBottom ? "Bottom" : "Top") + " | " + Vertices;
        }

        private void CalculateName()
        {
            // SB - naming convention for 2 triangles (bottom\top) in a size 3 grid:
            //      | A1\A2 | A3\A4 | A5\A6 |
            //      | B1\B2 | B3\B4 | B5\B6 |
            //      | C1\C2 | C3\C4 | C5\C6 |

            var invertedColumnIndex = ParentCell.ParentGrid.Size - ParentCell.Coordinates.Y + 1;

            var rowName = _triangleNameGenerator.NumberToName(invertedColumnIndex);

            var triangleNameIndex = ParentCell.Coordinates.X * GridCell.TrianglesPerCell;

            if (IsBottom)
            {
                triangleNameIndex -= 1;
            }

            Name = rowName + triangleNameIndex;
        }

        private Coordinates GetCoordinates(int vertexIndex)
        {
            // SB - Vertices indexes:
            //      Bottom    Top 
            //      2|\      3\ |1
            //      1|_\3      \|2

            if (vertexIndex == 1)
            {
                if (IsBottom)
                    return new Coordinates((ParentCell.Coordinates.X - 1) * NonHypotenuseSideLength, (ParentCell.Coordinates.Y - 1) * NonHypotenuseSideLength);
                return new Coordinates(ParentCell.Coordinates.X * NonHypotenuseSideLength, ParentCell.Coordinates.Y * NonHypotenuseSideLength);
            }
            else if (IsBottom && vertexIndex == 2 || !IsBottom && vertexIndex == 3)
            {
                return new Coordinates((ParentCell.Coordinates.X - 1) * NonHypotenuseSideLength, ParentCell.Coordinates.Y * NonHypotenuseSideLength);
            }
            else if (IsBottom && vertexIndex == 3 || !IsBottom && vertexIndex == 2)
            {
                return new Coordinates(ParentCell.Coordinates.X * NonHypotenuseSideLength, (ParentCell.Coordinates.Y - 1) * NonHypotenuseSideLength);
            }

            throw new InvalidOperationException($"Invalid triangle vertex: {vertexIndex}");
        }
    }
}
