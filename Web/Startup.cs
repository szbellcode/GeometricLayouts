using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.ModelServices;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // SB - document API using Swagger (http://localhost:5001/swagger)
            AddSwagger(services);

            // SB - custom DI registrations from Logic project
            services.AddLogicServices(Configuration);

            // SB - custom DI registrations for this app
            services.AddScoped<ISearchService, SearchService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // SB - url to the machine readable version of the swagger documentation 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geometric Layouts API");
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                // SB - overview info for the generated Swagger docs
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Geometric Layouts API"
                });

                var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Web.xml");
                setup.IncludeXmlComments(filePath);
            });
        }
    }
}
