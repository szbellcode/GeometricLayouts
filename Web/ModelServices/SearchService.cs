using Logic;
using Logic.Configuration;
using Logic.Shapes;
using Web.Models;

namespace Web.ModelServices
{
    public class SearchService : ISearchService
    {
        private readonly AppConfig _appConfig;
        private readonly IGridGenerator _gridGenerator;

        public SearchService(AppConfig appConfig, IGridGenerator gridGenerator)
        {
            _appConfig = appConfig;
            _gridGenerator = gridGenerator;
        }

        public ShapeSearchResultModel Search(ShapeSearchRequestModel search)
        {
            var grid = _gridGenerator.Generate(_appConfig.GridSize);

            ITriangle result = null;

            if (search.SearchBy.Equals("name", System.StringComparison.InvariantCultureIgnoreCase))
            {
                result = grid.FindTriangleByName(search.Name);
            }
            else if (search.SearchBy.Equals("coordinates", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var searchVertex = new TriangularVertex(new Coordinates(search.Vertex1), new Coordinates(search.Vertex2), new Coordinates(search.Vertex3));
                result = grid.FindTriangleByCoordinates(searchVertex);
            }

            if (result == null)
            {
                return null;
            }

            return new ShapeSearchResultModel
            {
                Name = result.Name,
                IsBottom = result.IsBottom,
                Vertex1 = result.Vertices.Vertex1,
                Vertex2 = result.Vertices.Vertex2,
                Vertex3 = result.Vertices.Vertex3,
                ParentCellX = result.ParentCell.Coordinates.X,
                ParentCellY = result.ParentCell.Coordinates.Y
        };
        }
    }
}
