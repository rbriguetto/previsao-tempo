using System.Net.Http.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PrevisaoTempo.Application;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;
using Xunit;

namespace PrevisaoTempo.IntegrationTests.Api;

public class CidadesControllerTests : IClassFixture<IntegrationTestsApplicationFactory>
{
    private readonly IntegrationTestsApplicationFactory _factory;

    public CidadesControllerTests(IntegrationTestsApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact()]
    public async Task TestaApiInsercao()
    {
        var sp = _factory.Services.CreateScope().ServiceProvider;
        var repositorioCidades = sp.GetRequiredService<IRepositorioCidades>();
     
        var command = new CriarCidadeCommand() { Nome = Guid.NewGuid().ToString(), Estado = "ES", 
            Latitude = new Random().Next(-100, 100), Longitude = new Random().Next(-100, 100) };
        var client = _factory.CreateClient();
        var response = await client.PostAsync("/api/Cidades/CriaCidade", JsonContent.Create(command));
        response.EnsureSuccessStatusCode();
    }

    [Fact()]
    public async Task TestApiAlteracao()
    {
        var sp = _factory.Services.CreateScope().ServiceProvider;
        var repositorioCidades = sp.GetRequiredService<IRepositorioCidades>();
        var cidadeOriginal = CriaCidadeTeste();

        await repositorioCidades.CriarCidadeAsync(cidadeOriginal);

        var random = new Random();

        var command = new AlterarCidadeCommand() { Id = cidadeOriginal.Id, 
            Nome = Guid.NewGuid().ToString(), 
            Estado = Guid.NewGuid().ToString(), 
            Latitude = random.Next(-100, 100), 
            Longitude = random.Next(-100, 100) };

        var client = _factory.CreateClient();
        var response = await client.PostAsync("/api/Cidades/AlteraCidade", JsonContent.Create(command));
        response.EnsureSuccessStatusCode();
    }

    [Fact()]
    public async Task TestaApiExclusao()
    {
        var sp = _factory.Services.CreateScope().ServiceProvider;
        var repositorioCidades = sp.GetRequiredService<IRepositorioCidades>();
        var cidade = CriaCidadeTeste();

        await repositorioCidades.CriarCidadeAsync(cidade);

        var client = _factory.CreateClient();
        var response = await client.DeleteAsync($"/api/Cidades/ExcluiCidade?id={cidade.Id}");
        response.EnsureSuccessStatusCode();
   }

    private Cidade CriaCidadeTeste()
    {
        var random = new Random();
        return new Cidade(Guid.NewGuid().ToString(), "ES", random.Next(-100, 100), random.Next(-100, 100));
    }
}