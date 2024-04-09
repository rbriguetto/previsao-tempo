using MediatR;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class ExcluirCidadeCommand: IRequest<bool>
{
    public int Id { get; set; } = 0;
}
