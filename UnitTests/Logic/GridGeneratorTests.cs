using Logic;
using Logic.Configuration;
using Logic.Shapes;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.Logic
{
    [TestFixture]
    public class GridGeneratorTests
    {
        [Test]
        public void Generate_WithInvalidMinGridSize_ThrowsException()
        {
            // Arrange
            const int MinSize = 5;
            const int MaxSize = 10;
            var sut = new GridGeneratorBuilder()
                .WithGridSizeConstraints(MinSize, MaxSize)
                .Build();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => sut.Generate(MinSize - 1));

            // Assert
            StringAssert.StartsWith($"Grid size must be between {MinSize} and {MaxSize}", ex.Message);
        }

        // Helper to build preconfigured Grid
        internal class GridGeneratorBuilder
        {
            AppConfig _appConfig;
            IGridCellFactory _gridCellFactory = new Mock<IGridCellFactory>().Object;

            public GridGeneratorBuilder()
            {
                // defaults
                WithGridSizeConstraints(1, 10);
            }

            public GridGeneratorBuilder WithGridSizeConstraints(int min, int max)
            {
                var stubConfig = new Mock<AppConfig>();
                stubConfig.SetupGet(x => x.GridSizeMin).Returns(min);
                stubConfig.SetupGet(x => x.GridSizeMax).Returns(max);

                _appConfig = stubConfig.Object;

                return this;
            }

            public GridGenerator Build()
            {
                return new GridGenerator(_gridCellFactory, _appConfig);
            }
        }
    }
}
