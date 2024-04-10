using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Infraestructure.Data;
using PrevisaoTempo.Infraestructure.OpenWeather;

namespace PrevisaoTempo.Infraestructure;

public static class ServiceCollectionExtension 
{
    public static IServiceCollection AddPrevisaoTempoInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()) );
        services.AddScoped<IRepositorioCidades, EfRepositorioCidades>();
        services.AddDbContext<PrevisaoTempoDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("PrevisaoTempo"));
        });
        services.Configure<OpenWeatherOptions>(configuration.GetSection(OpenWeatherOptions.Section));
        services.AddScoped<IServicoPrevisaoTempo, OpenWeatherApi>();
        return services;
    }
    
    public static IApplicationBuilder UsePrevisaoTempoInfraestructure(this IApplicationBuilder app, IServiceProvider services)
    {
        ConfigureRouteToServeSpaClient(app);

        using (var scope = services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PrevisaoTempoDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }

    public static void ConfigureRouteToServeSpaClient(IApplicationBuilder app)
    {
        app.Use(async (context, next) => {
            await next();
        
            // Serving index.html for 404 non api endpoints. Necessary
            // for angular routing
        
            if (context.Response.StatusCode != 404 || context.Request.Path.Value!.Contains("api/")) 
            {
                return;
            }
        
            string indexFileName = Path.Combine("wwwroot", "index.html");
            if (!File.Exists(indexFileName)) 
            {
                await context.Response.WriteAsync($"spaclient not found on wwwroot");
                return;
            }
            string text = File.ReadAllText(indexFileName);
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(text);
        });
    }
}