using Logic.Shapes;
using NUnit.Framework;
using System;

namespace UnitTests.Logic.Shapes
{
    [TestFixture]
    public class CoordinatesTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Constructor_WithNullOrEmptyString_ThrowsException(string value)
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Coordinates(value));

            // Assert
            StringAssert.Contains("coordinates", ex.Message);
        }

        [TestCase("1,")]
        [TestCase(",1")]
        [TestCase(",")]
        [TestCase("a,b")]
        public void Constructor_WithInvalidCoordinatesString_ThrowsException(string value)
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Coordinates(value));

            // Assert
            StringAssert.StartsWith("Invalid coordinates format", ex.Message);
        }

        [TestCase("1,1")]
        [TestCase(" 1,1")]
        [TestCase("1,1 ")]
        public void Constructor_WithValidCoordinatesString_IsSuccess(string value)
        {
            // Act
            var result = new Coordinates(value);

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
