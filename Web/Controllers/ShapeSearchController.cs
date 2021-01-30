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

        /// <summary>Searches for a triangle using the provided search criteria.</summary>
        /// <response code="200">Triangle found</response>
        /// <response code="400">Invalid search</response>
        /// <response code="404">Triangle not found</response>
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
