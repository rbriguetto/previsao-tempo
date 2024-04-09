using MediatR;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class RetornaCidadesQuery : IRequest<IList<Cidade>>
{

}