using System.Globalization;
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrevisaoTempo.Application.Exceptions;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Infraestructure.OpenWeather;

public class OpenWeatherApi : IServicoPrevisaoTempo
{
    private readonly OpenWeatherOptions _openWeatherOptions;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<OpenWeatherApi> _logger;

    public OpenWeatherApi(IOptions<OpenWeatherOptions> openWeatherOptions, 
        IMemoryCache memoryCache, 
        ILogger<OpenWeatherApi> logger)
    {
        _openWeatherOptions = openWeatherOptions.Value;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<Previsao> RetornaPrevisaoTempoAsync(Cidade cidade, CancellationToken cancellationToken = default)
    {
        var enUsCultureInfo = new CultureInfo("en-US");

        var endpoint = string.Format(_openWeatherOptions.ApiUrl, cidade.Latitude.ToString(enUsCultureInfo), 
            cidade.Longitude.ToString(enUsCultureInfo), _openWeatherOptions.ApiKey);

        if (_openWeatherOptions.UseLocalCache)
        {
            return await _memoryCache.GetOrCreateAsync(endpoint, entry => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_openWeatherOptions.CacheExpirationInSeconds);
                return CarregaPrevisaoDaApi(endpoint, cancellationToken);
            }) ?? throw new PrevisaoTempoException("Não foi posível obter a previsão para esta cidade");
        }

        return await CarregaPrevisaoDaApi(endpoint, cancellationToken);
    }

    public async Task<Previsao> CarregaPrevisaoDaApi(string endpoint, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Carregando informação do endpoint ${endpoint} ...");

        var httpClient = new HttpClient();
        var response = await httpClient.GetFromJsonAsync<OpenWeatherApiResponse>(endpoint, cancellationToken);

        if (response is null)
        {
            throw new PrevisaoTempoException("Ocorreu um erro ao acessar API");
        }

        return new Previsao() { 
            Temperatura = response.Main.Temp,
            SensacaoTermica = response.Main.FeelsLike,
            TemperaturaMaxima = response.Main.Max,
            TemperaturaMinima = response.Main.Min,
            Umidade = response.Main.Humidity,
            NascerDoSolUtc = response.Sys.Sunrise.UnixTimeToDateTime(),
            PorDoSolUtc = response.Sys.Sunset.UnixTimeToDateTime()
        };
    }
}
