using System.Collections.Generic;

namespace Logic.Shapes
{
    public interface IGridCell
    {
        Coordinates Coordinates { get; }
        IGrid ParentGrid { get; }
        IReadOnlyCollection<ITriangle> Triangles { get; }
    }
}