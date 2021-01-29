using System.Collections.Generic;

namespace Logic.Shapes
{
    public interface IGrid
    {
        IReadOnlyCollection<IGridCell> Cells { get; }
        int Size { get; }

        ITriangle FindTriangleByCoordinates(TriangularVertex vertex);
        ITriangle FindTriangleByName(string name);
    }
}