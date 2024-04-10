using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application.Services;

public interface IServicoPrevisaoTempo
{
    Task<Previsao> RetornaPrevisaoTempoAsync(Cidade cidade, CancellationToken cancellationToken = default);
}