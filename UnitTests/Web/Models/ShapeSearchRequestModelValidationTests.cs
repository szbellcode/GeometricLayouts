using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Web.Models;

namespace UnitTests.Web.Models
{
    [TestFixture]
    public class ShapeSearchRequestModelValidationTests
    {
        [Test]
        public void Validate_NameSearch_Valid()
        {
            // Arrange
            var sut = CreateValidNameSearchModel();

            // Act
            var validationResults = new List<ValidationResult>();
            var validationIsSuccess = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            // Assert
            Assert.IsTrue(validationIsSuccess);
            Assert.AreEqual(0, validationResults.Count);
        }

        [Test]
        public void Validate_CoordinateSearch_Valid()
        {
            // Arrange
            var sut = CreateValidCoordinateSearchModel();

            // Act
            var validationResults = new List<ValidationResult>();
            var validationIsSuccess = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            // Assert
            Assert.IsTrue(validationIsSuccess);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Validate_MissingSearchType_ReturnsError(string searchType)
        {
            // Arrange
            var sut = new ShapeSearchRequestModel() { SearchBy = searchType };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationIsSuccess = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            // Assert
            Assert.IsFalse(validationIsSuccess);
            Assert.AreEqual(1, validationResults.Count);
            
            var msg = validationResults[0];
            Assert.AreEqual("The SearchBy field is required.", msg.ErrorMessage);
        }

        [Test]
        public void Validate_CoordinateSearch_MissingCoordinate_ReturnsError()
        {
            // Arrange
            var sut = CreateValidCoordinateSearchModel();
            sut.Vertex1 = string.Empty;

            // Act
            var validationResults = new List<ValidationResult>();
            var validationIsSuccess = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            // Assert
            Assert.IsFalse(validationIsSuccess);
            Assert.AreEqual(1, validationResults.Count);
            
            var msg = validationResults[0];
            Assert.AreEqual("Please specify vertext coordinates.", msg.ErrorMessage);
            Assert.AreEqual(1, msg.MemberNames.Count());
            Assert.AreEqual("Vertex1", msg.MemberNames.First());
        }

        [TestCase("a,b")]
        [TestCase("1,b")]
        [TestCase("a,1")]
        [TestCase("1,")]
        [TestCase(",1")]
        [TestCase("*,*")]
        [TestCase("*,1")]
        public void Validate_CoordinateSearch_InvalidCoordinateFormat_ReturnsError(string invalidCoordinate)
        {
            // Arrange
            var sut = CreateValidCoordinateSearchModel();
            sut.Vertex1 = invalidCoordinate;

            // Act
            var validationResults = new List<ValidationResult>();
            var validationIsSuccess = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            // Assert
            Assert.IsFalse(validationIsSuccess);
            Assert.AreEqual(1, validationResults.Count);
            
            var msg = validationResults[0];
            Assert.AreEqual("Please specify vertext coordinates.", msg.ErrorMessage);
            Assert.AreEqual(1, msg.MemberNames.Count());
            Assert.AreEqual("Vertex1", msg.MemberNames.First());
        }

        /* ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ 
            SB - additional set of exhaustive tests 
            would go here .....
          ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ */

        private ShapeSearchRequestModel CreateValidNameSearchModel()
        {
            return new ShapeSearchRequestModel
            {
                SearchBy = "name",
                Name = "a1",
            };
        }

        private ShapeSearchRequestModel CreateValidCoordinateSearchModel()
        {
            return new ShapeSearchRequestModel
            {
                SearchBy = "coordinates",
                Vertex1 = "0,0",
                Vertex2 = "10,10",
                Vertex3 = "20,20"
            };
        }
    }
}
