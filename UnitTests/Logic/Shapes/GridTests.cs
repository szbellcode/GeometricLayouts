using Logic.Shapes;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Logic.Shapes
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void FindTriangleByName_WithValidName_ReturnsTriangle()
        {
            // Arrange
            var sut = new GridBuilder()
                .WithGridSize(2)
                .Build();

            // Act
            var result = sut.FindTriangleByName("11");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("11", result.Name);
        }

        [Test]
        public void FindTriangleByName_WithInvalidName_ReturnsNull()
        {
            // Arrange
            var sut = new GridBuilder()
                .WithGridSize(2)
                .Build();

            // Act
            var result = sut.FindTriangleByName("33");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void FindTriangleByCoordinates_WithValidVertices_ReturnsTriangle()
        {
            // Arrange
            var sut = new GridBuilder()
                .WithGridSize(4)
                .Build();

            // Act
            var coordinates = new Coordinates(4, 4);
            var result = sut.FindTriangleByCoordinates(new TriangularVertex(coordinates, coordinates, coordinates));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("44", result.Name);
            Assert.AreEqual(coordinates, result.Vertices.Vertex1);
            Assert.AreEqual(coordinates, result.Vertices.Vertex2);
            Assert.AreEqual(coordinates, result.Vertices.Vertex3);
        }

        [Test]
        public void FindTriangleByCoordinates_WithInvalidVertices_ReturnsNull()
        {
            // Arrange
            var sut = new GridBuilder()
                .WithGridSize(4)
                .Build();

            // Act
            var coordinates = new Coordinates(5, 5);
            var result = sut.FindTriangleByCoordinates(new TriangularVertex(coordinates, coordinates, coordinates));

            // Assert
            Assert.IsNull(result);
        }
    }

    // Helper to build preconfigured Grid
    internal class GridBuilder 
    {
        IGridCellFactory _gridCellFactory;
        int _gridSize;

        public GridBuilder()
        {
            _gridCellFactory = new Mock<IGridCellFactory>().Object;
            
            // default config
            WithGridSize(1);
        }

        public GridBuilder WithGridSize(int size)
        {
            _gridSize = size;

            // SB - create grid with cells count 'count^2' and fill each cell with 1 triangle which has:
            //       a name which matches has X,Y coordinates of cell e.g. 11, 12, 21, 22 (for 4 cell grid)
            //       a set of fake vertex coords related to cell e.g. for cell 11 (11, 11, 11)
            var stubGridCellFactory = new Mock<IGridCellFactory>();

            for (int gridCellX = 1; gridCellX <= size; gridCellX++)
            {
                for (int gridCellY = 1; gridCellY <= size; gridCellY++)
                {
                    var stubGridCell = new Mock<IGridCell>();
                    var cellTriangles = new List<ITriangle>();

                    var coordinates = new Coordinates(gridCellX, gridCellY);
                    
                    var stubTriangle = new Mock<ITriangle>();
                    stubTriangle.SetupGet(x => x.Name).Returns($"{gridCellX}{gridCellY}");
                    stubTriangle.SetupGet(x => x.Vertices).Returns(new TriangularVertex(coordinates, coordinates, coordinates));
                    cellTriangles.Add(stubTriangle.Object);
                    
                    stubGridCell.SetupGet(x => x.Triangles).Returns(cellTriangles.AsReadOnly());
                    stubGridCellFactory.Setup(x => x.Create(It.Is<Coordinates>(x => x == coordinates), It.IsAny<IGrid>())).Returns(stubGridCell.Object);
                }
            }

            _gridCellFactory = stubGridCellFactory.Object;

            return this;
        }

        public Grid Build()
        {
            return new Grid(_gridCellFactory, _gridSize);
        }
    }
}
