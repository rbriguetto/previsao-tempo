namespace PrevisaoTempo.Domain;

public record Previsao
{
    public readonly static Previsao Empty = new();

    public decimal Temperatura { get; set; }
    public decimal SensacaoTermica { get; set; } 
    public decimal TemperaturaMinima { get; set; } 
    public decimal TemperaturaMaxima { get; set; } 
    public decimal Umidade { get; set; } 
    public DateTime NascerDoSolUtc { get; set; } 
    public DateTime PorDoSolUtc { get; set; } 
}