namespace PrevisaoTempo.Infraestructure.OpenWeather;

public class OpenWeatherOptions
{
    public readonly static string Section = "OpenWeather";

    public string ApiUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public bool UseLocalCache { get; set; } = false;
    public int CacheExpirationInSeconds { get; set; } = 60;
}