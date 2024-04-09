using MediatR;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class RetornaCidadesQueryHandler : IRequestHandler<RetornaCidadesQuery, IList<Cidade>>
{
    private readonly IRepositorioCidades _repositorioCidades;

    public RetornaCidadesQueryHandler(IRepositorioCidades repositorioCidades)
    {
        _repositorioCidades = repositorioCidades;
    }

    public Task<IList<Cidade>> Handle(RetornaCidadesQuery request, CancellationToken cancellationToken)
    {
        return _repositorioCidades.RetornaCidadesAsync(cancellationToken);
    }
}