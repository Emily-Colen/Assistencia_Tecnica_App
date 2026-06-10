using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class StatusOrdemServicoService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public StatusOrdemServicoService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<StatusOrdemServico>> ListarAsync()
    {
        const string sql = @"
            SELECT
                idstatus AS IdStatus,
                nome AS Nome,
                descricao AS Descricao,
                finalizado AS Finalizado
            FROM status_ordem_servico
            ORDER BY idstatus;";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<StatusOrdemServico>(sql);
    }
}
