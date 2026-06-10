using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Helpers;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class TecnicoService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public TecnicoService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CriarTecnicoAsync(string nome, string email, string senha)
    {
        ValidationHelper.ValidarTextoObrigatorio(nome, nameof(nome));
        ValidationHelper.ValidarTextoObrigatorio(email, nameof(email));
        ValidationHelper.ValidarTextoObrigatorio(senha, nameof(senha));

        string senhaHash = PasswordHelper.GerarHashSha256(senha);

        const string sql = @"
            INSERT INTO tecnico (nome, email, senha_hash, ativo)
            VALUES (@Nome, @Email, @SenhaHash, 1);
            SELECT LAST_INSERT_ID();";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, new { Nome = nome, Email = email, SenhaHash = senhaHash });
    }

    public async Task<IEnumerable<Tecnico>> ListarAsync()
    {
        const string sql = @"
            SELECT idtecnico AS IdTecnico, nome AS Nome, email AS Email, senha_hash AS SenhaHash, ativo AS Ativo
            FROM tecnico
            ORDER BY nome;";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Tecnico>(sql);
    }
}
