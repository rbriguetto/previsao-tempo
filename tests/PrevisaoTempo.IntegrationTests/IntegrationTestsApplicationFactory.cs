using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Xunit;
using PrevisaoTempo.Infraestructure.Data;

namespace PrevisaoTempo.IntegrationTests;

public class IntegrationTestsApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PrevisaoTempoDbContext>));
            if (dbContext is not null)
            {
                services.Remove(dbContext);
            }

            services.AddDbContext<PrevisaoTempoDbContext>(options => options
                .UseSqlServer(_mssqlContainer.GetConnectionString()));
        });

        builder.UseEnvironment("Development");
    }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _mssqlContainer.StopAsync();
    }
}

