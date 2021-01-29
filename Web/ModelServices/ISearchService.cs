using Web.Models;

namespace Web.ModelServices
{
    public interface ISearchService
    {
        ShapeSearchResultModel Search(ShapeSearchRequestModel search);
    }
}
