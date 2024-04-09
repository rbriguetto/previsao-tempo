using MediatR;
using PrevisaoTempo.Application.Exceptions;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class CriarCidadeCommandHandler : IRequestHandler<CriarCidadeCommand, Cidade>
{
    private readonly IRepositorioCidades _repositorioCidades;

    public CriarCidadeCommandHandler(IRepositorioCidades repositorioCidades)
    {
        _repositorioCidades = repositorioCidades;
    }

    public async Task<Cidade> Handle(CriarCidadeCommand request, CancellationToken cancellationToken)
    {
        var novaCidade = new Cidade(request.Nome, request.Estado, request.Latitude, request.Longitude);

        if (await _repositorioCidades.RetornaCidadePorCoordenada(request.Latitude, request.Longitude, cancellationToken) == Cidade.Empty) 
        {
            throw new PrevisaoTempoException("Latitude / longitude já cadastradas");
        }

        await _repositorioCidades.CriarCidadeAsync(novaCidade, cancellationToken);

        return novaCidade;
    }
}