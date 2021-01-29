namespace Logic.Shapes
{
    public interface IGridCellFactory
    {
        IGridCell Create(Coordinates coordinates, IGrid parentGrid);
    }
}