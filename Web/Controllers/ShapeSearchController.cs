using Microsoft.AspNetCore.Mvc;
using System;
using Web.Models;
using Web.ModelServices;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapeSearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public ShapeSearchController(ISearchService searchService)
        {
            _searchService = searchService ?? throw new ArgumentException(nameof(searchService));
        }

        [HttpPost]
        public IActionResult Search(ShapeSearchRequestModel searchRequestModel)
        {
            ShapeSearchResultModel searchResult = _searchService.Search(searchRequestModel);

            if (searchResult == null)
            {
                return NotFound();
            }

            return Ok(searchResult);
        }
    }
}
