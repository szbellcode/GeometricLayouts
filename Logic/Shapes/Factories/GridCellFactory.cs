using System;

namespace Logic.Shapes
{
    public class GridCellFactory : IGridCellFactory
    {
        public GridCellFactory(ITriangleFactory triangleFactory)
        {
            _triangleFactory = triangleFactory ?? throw new ArgumentException(nameof(triangleFactory));
        }

        private readonly ITriangleFactory _triangleFactory;

        public IGridCell Create(Coordinates coordinates, IGrid parentGrid)
        {
            return new GridCell(_triangleFactory, parentGrid, coordinates);
        }
    }
}
