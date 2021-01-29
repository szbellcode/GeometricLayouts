using Logic.Shapes;

namespace Web.Models
{
    public class ShapeSearchResultModel
    {
        public string Name { get; set; }
        public bool IsBottom { get; set; }
        public Coordinates Vertex1 { get; set; }
        public Coordinates Vertex2 { get; set; }
        public Coordinates Vertex3 { get; set; }
        public int ParentCellX { get; set; }
        public int ParentCellY { get; set; }
    }
}
