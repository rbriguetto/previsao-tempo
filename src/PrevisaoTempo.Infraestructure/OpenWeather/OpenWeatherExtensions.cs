namespace PrevisaoTempo.Infraestructure.OpenWeather;

public static class OpenWeatherExtensions
{
    public static DateTime UnixTimeToDateTime(this long unixTime) =>
        new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(unixTime);
}