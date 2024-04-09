using System.ComponentModel.DataAnnotations.Schema;

namespace PrevisaoTempo.Infraestructure.Data;

[Table("Cidade")]
public class CidadeRecord
{
    public int Id { get; set; } = 0;
    public string Nome { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal Latitude { get; set; } = 0;
    public decimal Longitude { get; set; } = 0;
}