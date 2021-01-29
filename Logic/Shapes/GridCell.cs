using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Shapes
{
    /// <summary>
    /// A cell within a <see cref="Grid"/> which contains 2 <see cref="Triangle"/> shapes.
    /// </summary>
    public class GridCell : IGridCell
    {
        public GridCell(ITriangleFactory triangleFactory, IGrid parentGrid, Coordinates cellCoordinates)
        {
            _triangleFactory = triangleFactory ?? throw new ArgumentException(nameof(triangleFactory));
            Coordinates = cellCoordinates ?? throw new ArgumentException(nameof(cellCoordinates));
            if (parentGrid == null) { throw new ArgumentException(nameof(parentGrid)); }

            ParentGrid = parentGrid;

            GenerateTriangles();
        }

        public const int TrianglesPerCell = 2;
        private readonly ITriangleFactory _triangleFactory;
        private ITriangle[] _triangles;

        /// <summary>
        /// The {x,y} coordinates for this cell within its parent grid.
        /// </summary>
        public Coordinates Coordinates { get; private set; }

        /// <summary>
        /// Size of the parent grid.
        /// </summary>
        public IGrid ParentGrid { get; }

        /// <summary>
        /// The triangles contained within the cell.
        /// </summary>
        public IReadOnlyCollection<ITriangle> Triangles { get { return _triangles.ToList().AsReadOnly(); } }

        private void GenerateTriangles()
        {
            _triangles = new Triangle[TrianglesPerCell];

            _triangles[0] = _triangleFactory.Create(this, isBottom: true);
            _triangles[1] = _triangleFactory.Create(this, isBottom: false);
        }
    }
}