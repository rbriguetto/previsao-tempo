using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Infraestructure.Data;

public static class EfRepositorioExtensions
{
    public static CidadeRecord ToRecord(this Cidade cidade) => new CidadeRecord() { 
        Id = cidade.Id, 
        Nome = cidade.Nome, Estado = cidade.Estado, Latitude = cidade.Latitude, 
        Longitude = cidade.Longitude 
    };

    public static Cidade ToDomain(this CidadeRecord cidade) => new Cidade(cidade.Id, cidade.Nome, cidade.Estado, cidade.Latitude, cidade.Longitude);

}