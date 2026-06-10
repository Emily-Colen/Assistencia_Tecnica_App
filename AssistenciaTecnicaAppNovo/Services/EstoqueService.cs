using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Helpers;
using Dapper;
using MySqlConnector;

namespace AssistenciaTecnicaAppNovo.Services;

public class EstoqueService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public EstoqueService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task RegistrarEntradaAsync(int pecaId, int quantidade, decimal valorUnitario, int? tecnicoId, string? observacao)
    {
        await RegistrarMovimentacaoAsync("ENTRADA", pecaId, quantidade, valorUnitario, null, tecnicoId, observacao, somaEstoque: true);
    }

    public async Task RegistrarSaidaPorOrdemAsync(int pecaId, int ordemServicoId, int quantidade, decimal valorUnitario, int? tecnicoId, string? observacao)
    {
        await RegistrarMovimentacaoAsync("SAIDA", pecaId, quantidade, valorUnitario, ordemServicoId, tecnicoId, observacao, somaEstoque: false);
    }

    private async Task RegistrarMovimentacaoAsync(
        string tipo,
        int pecaId,
        int quantidade,
        decimal valorUnitario,
        int? ordemServicoId,
        int? tecnicoId,
        string? observacao,
        bool somaEstoque)
    {
        ValidationHelper.ValidarQuantidadePositiva(quantidade);
        ValidationHelper.ValidarValorNaoNegativo(valorUnitario, "valor unitário");

        await using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
        await conn.OpenAsync();
        await using var tx = await conn.BeginTransactionAsync();

        try
        {
            int estoqueAtual = await conn.ExecuteScalarAsync<int>(
                "SELECT estoque_atual FROM peca WHERE idpeca = @PecaId FOR UPDATE;",
                new { PecaId = pecaId }, tx);

            if (!somaEstoque && estoqueAtual < quantidade)
                throw new InvalidOperationException("Estoque insuficiente para realizar a saída.");

            int novoEstoque = somaEstoque ? estoqueAtual + quantidade : estoqueAtual - quantidade;

            await conn.ExecuteAsync(
                "UPDATE peca SET estoque_atual = @NovoEstoque WHERE idpeca = @PecaId;",
                new { NovoEstoque = novoEstoque, PecaId = pecaId }, tx);

            await conn.ExecuteAsync(@"
                INSERT INTO movimentacao_estoque
                (tipo_movimentacao, quantidade, valor_unitario, observacao, peca_idpeca, ordem_servico_idordem_servico, tecnico_idtecnico)
                VALUES
                (@Tipo, @Quantidade, @ValorUnitario, @Observacao, @PecaId, @OrdemServicoId, @TecnicoId);",
                new
                {
                    Tipo = tipo,
                    Quantidade = quantidade,
                    ValorUnitario = valorUnitario,
                    Observacao = observacao,
                    PecaId = pecaId,
                    OrdemServicoId = ordemServicoId,
                    TecnicoId = tecnicoId
                }, tx);

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}
