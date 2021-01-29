namespace Logic.Shapes
{
    public interface ITriangleFactory
    {
        Triangle Create(GridCell parentCell, bool isBottom);
    }
}