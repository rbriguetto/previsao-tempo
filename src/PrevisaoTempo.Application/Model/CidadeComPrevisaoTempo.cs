using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application.Models;

public record CidadeComPrevisaoTempo
{
    public Cidade Cidade { get; set; } = Cidade.Empty;
    public Previsao PrevisaoTempo { get; set; } = Previsao.Empty;
}