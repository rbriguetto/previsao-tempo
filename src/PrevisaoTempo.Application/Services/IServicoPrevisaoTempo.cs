using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Application.Services;

public interface IServicoPrevisaoTempo
{
    Task<Previsao> RetornaPrevisaoTempo(Cidade cidade, CancellationToken cancellationToken = default);
}