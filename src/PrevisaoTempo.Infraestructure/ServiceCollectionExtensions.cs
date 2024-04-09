using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Infraestructure.Data;

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
        return services;
    }
}