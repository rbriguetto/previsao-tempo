using MediatR;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class CriarCidadeCommand : IRequest<Cidade>
{
    public string Nome { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal Latitude { get; set; } = 0;
    public decimal Longitude { get; set; } = 0;
}