using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application.Services;

public interface IRepositorioCidades
{
    public Task CriarCidadeAsync(Cidade cidade, CancellationToken cancellationToken = default);
    public Task<bool> RetornaCidadePorCoordenada(decimal latitude, decimal longitude, CancellationToken cancellationToken);
}