using Logic.Configuration;
using Logic.Shapes;
using Logic.Shapes.Naming;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Logic.Shapes
{
    [TestFixture]
    public class TriangleTests
    {
        [Test]
        public void Name_ForBottomTriangleIn1GridCell_ReturnsNameWith1Suffix()
        {
            // Arrange
            var sut = new TriangleBuilder()
                .WithIsTriangleOnBottom(true)
                .Build();

            // Act
            var result = sut.Name;

            // Assert
            Assert.AreEqual($"{TriangleBuilder.TriangleName}1", result);
        }

        [Test]
        public void Name_ForTopTriangleIn1GridCell_ReturnsNameWith2Suffix()
        {
            // Arrange
            var sut = new TriangleBuilder()
                .WithIsTriangleOnBottom(false)
                .Build();

            // Act
            var result = sut.Name;

            // Assert
            Assert.AreEqual($"{TriangleBuilder.TriangleName}2", result);
        }

        [TestCaseSource(nameof(TriangleCoordTestCases))]
        public void Vertices_ExhaustiveTestForAllTrianglesIn3By3Grid(int parentCellX, int parentCellY, bool isBottom, TriangularVertex expectedVertex)
        {
            // Arrange
            var parentCell = new Mock<IGridCell>();
            parentCell.Setup(x => x.Coordinates).Returns(new Coordinates(parentCellX, parentCellY));
            parentCell.Setup(x => x.ParentGrid.Size).Returns(3);

            var sut = new TriangleBuilder()
                .WithIsTriangleOnBottom(isBottom)
                .WithNonHypotenuseSideLength(10)
                .WithParentGridCell(parentCell)
                .Build();

            // Act
            var result = sut.Vertices;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedVertex.Vertex1, result.Vertex1, "Vertex1");
            Assert.AreEqual(expectedVertex.Vertex2, result.Vertex2, "Vertex2");
            Assert.AreEqual(expectedVertex.Vertex3, result.Vertex3, "Vertex3");
        }

        private static IEnumerable<TestCaseData> TriangleCoordTestCases
        {
            get
            {
                // bottom triangles
                yield return new TestCaseData(1, 1, true, new TriangularVertex(new Coordinates(0, 0), new Coordinates(0, 10), new Coordinates(10, 0))).SetName("Cell B1,1");
                yield return new TestCaseData(1, 2, true, new TriangularVertex(new Coordinates(0, 10), new Coordinates(0, 20), new Coordinates(10, 10))).SetName("Cell B1,2");
                yield return new TestCaseData(1, 3, true, new TriangularVertex(new Coordinates(0, 20), new Coordinates(0, 30), new Coordinates(10, 20))).SetName("Cell B1,3");
                yield return new TestCaseData(2, 1, true, new TriangularVertex(new Coordinates(10, 0), new Coordinates(10, 10), new Coordinates(20, 0))).SetName("Cell B2,1");
                yield return new TestCaseData(2, 2, true, new TriangularVertex(new Coordinates(10, 10), new Coordinates(10, 20), new Coordinates(20, 10))).SetName("Cell B2,2");
                yield return new TestCaseData(2, 3, true, new TriangularVertex(new Coordinates(10, 20), new Coordinates(10, 30), new Coordinates(20, 20))).SetName("Cell B2,3");
                yield return new TestCaseData(3, 1, true, new TriangularVertex(new Coordinates(20, 0), new Coordinates(20, 10), new Coordinates(30, 0))).SetName("Cell B3,1");
                yield return new TestCaseData(3, 2, true, new TriangularVertex(new Coordinates(20, 10), new Coordinates(20, 20), new Coordinates(30, 10))).SetName("Cell B3,2");
                yield return new TestCaseData(3, 3, true, new TriangularVertex(new Coordinates(20, 20), new Coordinates(20, 30), new Coordinates(30, 20))).SetName("Cell B3,3");

                // top triangles
                yield return new TestCaseData(1, 1, false, new TriangularVertex(new Coordinates(10, 10), new Coordinates(10, 0), new Coordinates(0, 10))).SetName("Cell T1,1");
                yield return new TestCaseData(1, 2, false, new TriangularVertex(new Coordinates(10, 20), new Coordinates(10, 10), new Coordinates(0, 20))).SetName("Cell T1,2");
                yield return new TestCaseData(1, 3, false, new TriangularVertex(new Coordinates(10, 30), new Coordinates(10, 20), new Coordinates(0, 30))).SetName("Cell T1,3");
                yield return new TestCaseData(2, 1, false, new TriangularVertex(new Coordinates(20, 10), new Coordinates(20, 0), new Coordinates(10, 10))).SetName("Cell T2,1");
                yield return new TestCaseData(2, 2, false, new TriangularVertex(new Coordinates(20, 20), new Coordinates(20, 10), new Coordinates(10, 20))).SetName("Cell T2,2");
                yield return new TestCaseData(2, 3, false, new TriangularVertex(new Coordinates(20, 30), new Coordinates(20, 20), new Coordinates(10, 30))).SetName("Cell T2,3");
                yield return new TestCaseData(3, 1, false, new TriangularVertex(new Coordinates(30, 10), new Coordinates(30, 0), new Coordinates(20, 10))).SetName("Cell T3,1");
                yield return new TestCaseData(3, 2, false, new TriangularVertex(new Coordinates(30, 20), new Coordinates(30, 10), new Coordinates(20, 20))).SetName("Cell T3,2");
                yield return new TestCaseData(3, 3, false, new TriangularVertex(new Coordinates(30, 30), new Coordinates(30, 20), new Coordinates(20, 30))).SetName("Cell T3,3");

            }
        }
    }

    // helper to build preconfigured Triangle
    internal class TriangleBuilder
    {
        public const string TriangleName = "Name";
        bool IsBottom;

        Mock<AppConfig> _stubAppConfig;
        Mock<ITriangleNameGenerator> _stubNameGenerator;
        Mock<IGridCell> _parentCell;

        public TriangleBuilder()
        {
            _stubAppConfig = new Mock<AppConfig>();
            _stubNameGenerator = new Mock<ITriangleNameGenerator>();
            _stubNameGenerator.Setup(x => x.NumberToName(It.IsAny<int>())).Returns(TriangleName);   // all triangles have fixed name

            // default config (create single triangle in bottom position of a grid cell in grid location (1,1))
            _parentCell = new Mock<IGridCell>();
            _parentCell.Setup(x => x.ParentGrid.Size).Returns(1);
            _parentCell.Setup(x => x.Coordinates).Returns(new Coordinates(1, 1));

            WithIsTriangleOnBottom(true);
        }

        public TriangleBuilder WithParentGridCell(Mock<IGridCell> gridCell)
        {
            _parentCell = gridCell;
            return this;
        }

        public TriangleBuilder WithNonHypotenuseSideLength(int length)
        {
            _stubAppConfig.SetupGet(x => x.NonHypotenuseSideLength).Returns(length);
            return this;
        }

        public TriangleBuilder WithIsTriangleOnBottom(bool isBottom)
        {
            IsBottom = isBottom;
            return this;
        }

        public Triangle Build()
        {
            return new Triangle(_stubNameGenerator.Object, _stubAppConfig.Object, _parentCell.Object, IsBottom);
        }
    }
}
