using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Helpers;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class EquipamentoService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public EquipamentoService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Equipamento>> ListarPorClienteAsync(int clienteId)
    {
        const string sql = @"
            SELECT
                idequipamento AS IdEquipamento,
                tipo AS Tipo,
                marca AS Marca,
                modelo AS Modelo,
                numero_serie AS NumeroSerie,
                descricao AS Descricao,
                cliente_idcliente AS ClienteIdCliente
            FROM equipamento
            WHERE cliente_idcliente = @ClienteId
            ORDER BY tipo, marca, modelo;";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Equipamento>(sql, new { ClienteId = clienteId });
    }

    public async Task<int> InserirAsync(Equipamento equipamento)
    {
        ValidationHelper.ValidarTextoObrigatorio(equipamento.Tipo, "tipo");
        if (equipamento.ClienteIdCliente <= 0) throw new ArgumentException("Cliente inválido.");

        const string sql = @"
            INSERT INTO equipamento (tipo, marca, modelo, numero_serie, descricao, cliente_idcliente)
            VALUES (@Tipo, @Marca, @Modelo, @NumeroSerie, @Descricao, @ClienteIdCliente);
            SELECT LAST_INSERT_ID();";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, equipamento);
    }
}
