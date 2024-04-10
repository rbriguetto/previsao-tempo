using System.Globalization;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PrevisaoTempo.Application.Exceptions;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Infraestructure.OpenWeather;

public class OpenWeatherApi : IServicoPrevisaoTempo
{
    private readonly OpenWeatherOptions _openWeatherOptions;

    public OpenWeatherApi(IOptions<OpenWeatherOptions> openWeatherOptions)
    {
        _openWeatherOptions = openWeatherOptions.Value;
    }

    public async Task<Previsao> RetornaPrevisaoTempoAsync(Cidade cidade, CancellationToken cancellationToken = default)
    {
        var enUsCultureInfo = new CultureInfo("en-US");
        var endpoint = string.Format(_openWeatherOptions.ApiUrl, cidade.Latitude.ToString(enUsCultureInfo), 
            cidade.Longitude.ToString(enUsCultureInfo), _openWeatherOptions.ApiKey);
        var httpClient = new HttpClient();
        var response = await httpClient.GetFromJsonAsync<OpenWeatherApiResponse>(endpoint);

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
