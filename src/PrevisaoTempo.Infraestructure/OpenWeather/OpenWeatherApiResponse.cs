using System.Text.Json.Serialization;

namespace PrevisaoTempo.Infraestructure.OpenWeather;

public class OpenWeatherApiResponse
{
    public OpenWeatherApiResponseMain Main { get; set; } = new();
    public OpenWeatherApiResponseSys Sys { get; set; } = new();
}

public class OpenWeatherApiResponseMain
{
    public decimal Temp { get; set; } = 0;

    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; } = 0;

    [JsonPropertyName("temp_min")]
    public decimal Min { get; set; } = 0;

    [JsonPropertyName("temp_max")]
    public decimal Max { get; set; } = 0;
    public decimal Humidity { get; set; } = 0;
}

public class OpenWeatherApiResponseSys
{
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
}