using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Shapes
{
    /// <summary>
    /// A Square Grid shape containing a configurable number of <see cref="GridCell"/>s each of which contain two <see cref="Triangle"/> shapes.
    /// </summary>
    public class Grid : IGrid
    {
        public Grid(IGridCellFactory gridCellFactory, int size)
        {
            _gridCellFactory = gridCellFactory ?? throw new ArgumentException(nameof(gridCellFactory));
            Size = size;

            Generate();
        }

        private readonly IGridCellFactory _gridCellFactory;
        private HashSet<IGridCell> _cells;

        /// <summary>
        /// Each of the cells contained in the grid.
        /// </summary>
        public IReadOnlyCollection<IGridCell> Cells { get { return _cells.ToList().AsReadOnly(); } }

        /// <summary>
        /// Grid size (the number of cells the grid will contain will be this number squared).
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Locate a triangle by its given name.
        /// </summary>
        /// <param name="name">The name of the triangle to search for.</param>
        /// <returns>The located triangle or null if not found.</returns>
        public ITriangle FindTriangleByName(string name)
        {
            return Cells.SelectMany(c => c.Triangles).FirstOrDefault(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Locate a triangle using its coordinates within the grid.
        /// </summary>
        /// <param name="vertex">Set of 3 coordinates dictating the position of the triangle to search for.</param>
        /// <returns>The located triangle or null if not found.</returns>
        public ITriangle FindTriangleByCoordinates(TriangularVertex vertex)
        {
            return Cells.SelectMany(c => c.Triangles).FirstOrDefault(t => t.Vertices.Equals(vertex));
        }

        private void Generate()
        {
            _cells = new HashSet<IGridCell>();

            for (int gridCellX = 1; gridCellX <= Size; gridCellX++)
            {
                for (int gridCellY = 1; gridCellY <= Size; gridCellY++)
                {
                    _cells.Add(_gridCellFactory.Create(new Coordinates(gridCellX, gridCellY), this));
                }
            }
        }
    }
}
