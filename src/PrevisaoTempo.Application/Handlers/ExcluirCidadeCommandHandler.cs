using MediatR;
using PrevisaoTempo.Application.Services;

namespace PrevisaoTempo.Application;

public class ExcluirCidadeCommandHandler : IRequestHandler<ExcluirCidadeCommand, bool>
{
    private readonly IRepositorioCidades _repositorioCidades;

    public ExcluirCidadeCommandHandler(IRepositorioCidades repositorioCidades)
    {
        _repositorioCidades = repositorioCidades;
    }

    public async Task<bool> Handle(ExcluirCidadeCommand request, CancellationToken cancellationToken)
    {
        await _repositorioCidades.ExcluirCidadeAsync(request.Id, cancellationToken);

        return true;
    }
}