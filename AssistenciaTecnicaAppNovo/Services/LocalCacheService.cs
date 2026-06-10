using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Models;
using SQLite;

namespace AssistenciaTecnicaAppNovo.Services;

public class LocalCacheService
{
    private readonly SQLiteConnectionFactory _sqliteFactory;

    public LocalCacheService(SQLiteConnectionFactory sqliteFactory)
    {
        _sqliteFactory = sqliteFactory;
    }

    public async Task InicializarAsync()
    {
        var db = _sqliteFactory.CreateConnection();
        await db.CreateTableAsync<PecaCache>();
    }

    public async Task SalvarPecasCacheAsync(IEnumerable<Peca> pecas)
    {
        var db = _sqliteFactory.CreateConnection();
        await InicializarAsync();
        await db.DeleteAllAsync<PecaCache>();

        foreach (var peca in pecas)
        {
            await db.InsertAsync(new PecaCache
            {
                IdPeca = peca.IdPeca,
                Nome = peca.Nome,
                EstoqueAtual = peca.EstoqueAtual
            });
        }
    }
}

public class PecaCache
{
    [PrimaryKey]
    public int IdPeca { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int EstoqueAtual { get; set; }
}
