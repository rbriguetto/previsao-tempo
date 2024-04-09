using Microsoft.EntityFrameworkCore;

namespace PrevisaoTempo.Infraestructure.Data;

public class PrevisaoTempoDbContext : DbContext
{
    public DbSet<CidadeRecord> Cidades { get; set; }

}
