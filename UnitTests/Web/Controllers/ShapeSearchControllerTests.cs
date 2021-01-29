using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;
using Web.ModelServices;

namespace UnitTests.Web.Controllers
{
    [TestFixture]
    public class ShapeSearchControllerTests
    {
        [Test]
        public void Search_NoSearchResult_Returns404()
        {
            // Arrange
            var stubSearchService = new Mock<ISearchService>();
            stubSearchService.Setup(x => x.Search(It.IsAny<ShapeSearchRequestModel>())).Returns<ShapeSearchResultModel>(null);
            var sut = new ShapeSearchController(stubSearchService.Object);

            // Act
            var result = sut.Search(new ShapeSearchRequestModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Search_HasSearchResult_Returns200()
        {
            // Arrange
            var stubSearchService = new Mock<ISearchService>();
            stubSearchService.Setup(x => x.Search(It.IsAny<ShapeSearchRequestModel>())).Returns(new ShapeSearchResultModel());
            var sut = new ShapeSearchController(stubSearchService.Object);

            // Act
            var result = sut.Search(new ShapeSearchRequestModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
