using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application.Services;

public interface IRepositorioCidades
{
    public Task CriarCidadeAsync(Cidade cidade, CancellationToken cancellationToken = default);
    public Task AlterarCidadeAsync(Cidade cidade, CancellationToken cancellationToken = default);
    public Task ExcluirCidadeAsync(int id, CancellationToken cancellationToken = default);
    public Task<Cidade> RetornaCidadePorCoordenada(decimal latitude, decimal longitude, CancellationToken cancellationToken);
    public Task<IList<Cidade>> RetornaCidadesAsync(CancellationToken cancellationToken);
}