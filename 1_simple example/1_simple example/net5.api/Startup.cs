using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using simple.shared.Data;
using simple.shared.Data.Repositories;
using simple.shared.Interfaces;
using simple.shared.Services;
using simple.shared.Utils;

namespace net5.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IDeveloperRepository, DeveloperRepository>();
            services.AddTransient<IDeveloperService, DeveloperService>();

            services.AddDbContext<DataContext>(options
                => options.UseSqlServer(Config.Database));

            services.AddSwaggerGen(configs =>
                configs.SwaggerDoc(Config.Version, new OpenApiInfo { Title = Config.Net5, Version = Config.Version }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Config.Net5} {Config.Version}"));

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
