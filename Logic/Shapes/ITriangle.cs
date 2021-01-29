namespace Logic.Shapes
{
    public interface ITriangle
    {
        bool IsBottom { get; }
        string Name { get; }
        TriangularVertex Vertices { get; }
        IGridCell ParentCell { get; }
    }
}