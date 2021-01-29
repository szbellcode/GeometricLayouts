using Logic.Configuration;
using Logic.Shapes;
using Logic.Shapes.Naming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logic
{
    public static class LogicServiceRegistrations
    {
        public static IServiceCollection AddLogicServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGridGenerator, GridGenerator>();
            services.AddScoped<IGridCellFactory, GridCellFactory>();
            services.AddScoped<ITriangleFactory, TriangleFactory>();
            services.AddScoped<ITriangleNameGenerator, Base26AlphabetNumbers>();

            // Read appsettings.json into custom config class
            AppConfig appConfig = new AppConfig();
            configuration.GetSection("App").Bind(appConfig);
            services.AddSingleton<AppConfig>(appConfig);

            return services;
        }
    }
}
