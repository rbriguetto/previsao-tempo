using MediatR;
using PrevisaoTempo.Application.Exceptions;
using PrevisaoTempo.Application.Models;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class ConsultaPrevisaoTempoQueryHandler : IRequestHandler<ConsultaPrevisaoTempoQuery, CidadeComPrevisaoTempo>
{
    private readonly IRepositorioCidades _repositorioCidades;
    private readonly IServicoPrevisaoTempo _servicoPrevisaoTempo;

    public ConsultaPrevisaoTempoQueryHandler(IRepositorioCidades repositorioCidades, IServicoPrevisaoTempo servicoPrevisaoTempo)
    {
        _repositorioCidades = repositorioCidades;
        _servicoPrevisaoTempo = servicoPrevisaoTempo;
    }

    public async Task<CidadeComPrevisaoTempo> Handle(ConsultaPrevisaoTempoQuery request, CancellationToken cancellationToken)
    {
        var cidade = await _repositorioCidades.RetornaCidadePorIdAsync(request.IdCidade, cancellationToken);

        if (cidade == Cidade.Empty)
        {
            throw new PrevisaoTempoException("Cidade não encontrada");
        }

        return new CidadeComPrevisaoTempo() { 
            Cidade = cidade,
            PrevisaoTempo = await _servicoPrevisaoTempo.RetornaPrevisaoTempo(cidade)
        };
    }
}