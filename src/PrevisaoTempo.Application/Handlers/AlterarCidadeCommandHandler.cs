using MediatR;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application;

public class AlterarCidadeCommandHandler : IRequestHandler<AlterarCidadeCommand, Cidade>
{
    private readonly IRepositorioCidades _repositorioCidades;

    public AlterarCidadeCommandHandler(IRepositorioCidades repositorioCidades)
    {
        _repositorioCidades = repositorioCidades;
    }

    public async Task<Cidade> Handle(AlterarCidadeCommand request, CancellationToken cancellationToken)
    {
        var cidadeAlterada = new Cidade(request.Id, request.Nome, request.Estado, request.Latitude, request.Longitude);

        await _repositorioCidades.AlterarCidadeAsync(cidadeAlterada, cancellationToken);

        return cidadeAlterada;
    }
}