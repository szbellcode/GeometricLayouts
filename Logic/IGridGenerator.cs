using Logic.Shapes;

namespace Logic
{
    public interface IGridGenerator
    {
        Grid Generate(int size);
    }
}