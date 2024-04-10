using MediatR;
using PrevisaoTempo.Application.Models;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class ConsultaPrevisaoTempoQuery: IRequest<CidadeComPrevisaoTempo>
{
    public int IdCidade { get; set ;}
}