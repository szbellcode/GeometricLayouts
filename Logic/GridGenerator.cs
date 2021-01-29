using Console.Extensions;
using Logic.Configuration;
using Logic.Shapes;
using System;

namespace Logic
{
    public class GridGenerator : IGridGenerator
    {
        private readonly IGridCellFactory _gridCellFactory;
        private readonly AppConfig _appConfig;

        public GridGenerator(IGridCellFactory gridCellFactory, AppConfig appConfig)
        {
            _gridCellFactory = gridCellFactory ?? throw new ArgumentException(nameof(gridCellFactory));
            _appConfig = appConfig ?? throw new ArgumentException(nameof(appConfig));
        }

        public Grid Generate(int size)
        {
            if (!size.BetweenInclusive(_appConfig.GridSizeMin, _appConfig.GridSizeMax))
                throw new ArgumentException($"Grid size must be between {_appConfig.GridSizeMin} and {_appConfig.GridSizeMax}", nameof(size));

            return new Grid(_gridCellFactory, size);
        }
    }
}
