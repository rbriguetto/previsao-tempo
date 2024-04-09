using Microsoft.EntityFrameworkCore;
using PrevisaoTempo.Application.Exceptions;
using PrevisaoTempo.Application.Services;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Infraestructure.Data;

public class EfRepositorioCidades : IRepositorioCidades
{
    private readonly PrevisaoTempoDbContext _dbContext;

    public EfRepositorioCidades(PrevisaoTempoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CriarCidadeAsync(Cidade cidade, CancellationToken cancellationToken = default)
    {
        var cidadeRecord = cidade.ToRecord();
        _dbContext.Cidades.Add(cidadeRecord);
        await _dbContext.SaveChangesAsync(cancellationToken);
        cidade.Id = cidadeRecord.Id;
    }
 
    public async Task AlterarCidadeAsync(Cidade cidade, CancellationToken cancellationToken = default)
    {
        var cidadeRecord = await _dbContext.Cidades.FindAsync(cidade.Id, cancellationToken);

        if (cidadeRecord is null)
        {
            throw new PrevisaoTempoException("Cidade não encontrada");
        }

        cidadeRecord.Nome = cidade.Nome;
        cidadeRecord.Estado = cidade.Estado;
        cidadeRecord.Latitude = cidade.Latitude;
        cidadeRecord.Longitude = cidade.Longitude;

        _dbContext.Update(cidadeRecord);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ExcluirCidadeAsync(int id, CancellationToken cancellationToken = default)
    {
        var cidadeRecord = await _dbContext.Cidades.FindAsync(id, cancellationToken);

        if (cidadeRecord is null)
        {
            throw new PrevisaoTempoException("Cidade não encontrada");
        }

        _dbContext.Cidades.Remove(cidadeRecord);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Cidade> RetornaCidadePorCoordenada(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        var cidadeRecord = await _dbContext.Cidades.Where(cidade => cidade.Latitude == latitude && cidade.Longitude == longitude)
            .FirstOrDefaultAsync(cancellationToken);

        if (cidadeRecord is null)
        {
            return Cidade.Empty;
        }

        return cidadeRecord.ToDomain();
    }

    public async Task<IList<Cidade>> RetornaCidadesAsync(CancellationToken cancellationToken)
    {
        return (await _dbContext
            .Cidades
            .AsNoTracking()
            .ToListAsync(cancellationToken))
            .Select(cidade => cidade.ToDomain())
            .ToList();
    }
}
