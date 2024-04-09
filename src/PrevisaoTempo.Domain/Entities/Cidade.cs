namespace PrevisaoTempo.Domain;

public record Cidade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Estado { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public Cidade(int id, string nome, string estado, decimal latitude, decimal longitude)
    {
        Id = id;
        Nome = nome;
        Estado = estado;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Cidade(string nome, string estado, decimal latitude, decimal longitude)
    {
        Id = -1;
        Nome = nome;
        Estado = estado;
        Latitude = latitude;
        Longitude = longitude;
    }
}