using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.DTOs;
using MongoDB.Driver;

namespace AssistenciaTecnicaAppNovo.Services;

public class MongoAnexoService
{
    private readonly IMongoCollection<AnexoMongo> _collection;

    public MongoAnexoService(MongoConnectionFactory mongoConnectionFactory)
    {
        _collection = mongoConnectionFactory
            .GetDatabase()
            .GetCollection<AnexoMongo>("anexos_ordem_servico");
    }

    public async Task SalvarAnexoAsync(AnexoMongo anexo)
    {
        if (anexo.OrdemServicoId <= 0)
            throw new ArgumentException("Ordem de serviço inválida.");

        await _collection.InsertOneAsync(anexo);
    }

    public async Task<List<AnexoMongo>> ListarPorOrdemServicoAsync(int ordemServicoId)
    {
        return await _collection
            .Find(a => a.OrdemServicoId == ordemServicoId)
            .SortByDescending(a => a.DataUpload)
            .ToListAsync();
    }
}
