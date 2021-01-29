using Logic.Configuration;
using Logic.Shapes.Naming;
using System;

namespace Logic.Shapes
{
    public class TriangleFactory : ITriangleFactory
    {
        public TriangleFactory(ITriangleNameGenerator triangleNameGenerator, AppConfig appConfig)
        {
            _triangleNameGenerator = triangleNameGenerator ?? throw new ArgumentException(nameof(triangleNameGenerator));
            _appConfig = appConfig ?? throw new ArgumentException(nameof(appConfig));
        }

        private readonly ITriangleNameGenerator _triangleNameGenerator;
        private readonly AppConfig _appConfig;

        public Triangle Create(GridCell parentCell, bool isBottom)
        {
            return new Triangle(_triangleNameGenerator, _appConfig, parentCell, isBottom);
        }
    }
}
