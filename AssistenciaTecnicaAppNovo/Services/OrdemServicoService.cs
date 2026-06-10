using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Helpers;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class OrdemServicoService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public OrdemServicoService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<OrdemServico>> ListarAsync()
    {
        const string sql = @"
            SELECT
                idordem_servico AS IdOrdemServico,
                defeito_relatado AS DefeitoRelatado,
                preco AS Preco,
                data_abertura AS DataAbertura,
                data_fechamento AS DataFechamento,
                data_prevista AS DataPrevista,
                descricao_manutencao AS DescricaoManutencao,
                status_idstatus AS StatusIdStatus,
                tecnico_idtecnico AS TecnicoIdTecnico,
                equipamento_idequipamento AS EquipamentoIdEquipamento
            FROM ordem_servico
            ORDER BY data_abertura DESC;";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<OrdemServico>(sql);
    }

    public async Task<int> AbrirAsync(OrdemServico ordem)
    {
        ValidationHelper.ValidarTextoObrigatorio(ordem.DefeitoRelatado, "defeito relatado");
        ValidationHelper.ValidarValorNaoNegativo(ordem.Preco, "preço");

        if (ordem.StatusIdStatus <= 0) throw new ArgumentException("Status inválido.");
        if (ordem.EquipamentoIdEquipamento <= 0) throw new ArgumentException("Equipamento inválido.");

        const string sql = @"
            INSERT INTO ordem_servico
            (defeito_relatado, preco, data_prevista, descricao_manutencao, status_idstatus, tecnico_idtecnico, equipamento_idequipamento)
            VALUES
            (@DefeitoRelatado, @Preco, @DataPrevista, @DescricaoManutencao, @StatusIdStatus, @TecnicoIdTecnico, @EquipamentoIdEquipamento);
            SELECT LAST_INSERT_ID();";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, ordem);
    }

    public async Task AtualizarStatusAsync(int ordemServicoId, int statusId, DateTime? dataFechamento = null)
    {
        const string sql = @"
            UPDATE ordem_servico
            SET status_idstatus = @StatusId,
                data_fechamento = @DataFechamento
            WHERE idordem_servico = @OrdemServicoId;";

        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(sql, new { OrdemServicoId = ordemServicoId, StatusId = statusId, DataFechamento = dataFechamento });
    }

    public async Task AdicionarPecaAsync(int ordemServicoId, int pecaId, int quantidade, decimal valorUnitario)
    {
        ValidationHelper.ValidarQuantidadePositiva(quantidade);
        ValidationHelper.ValidarValorNaoNegativo(valorUnitario, "valor unitário");

        decimal subtotal = quantidade * valorUnitario;

        const string sql = @"
            INSERT INTO peca_has_ordem_servico
            (peca_idpeca, ordem_servico_idordem_servico, quantidade, subtotal, valor_unitario)
            VALUES
            (@PecaId, @OrdemServicoId, @Quantidade, @Subtotal, @ValorUnitario);";

        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(sql, new { PecaId = pecaId, OrdemServicoId = ordemServicoId, Quantidade = quantidade, Subtotal = subtotal, ValorUnitario = valorUnitario });
    }
}
