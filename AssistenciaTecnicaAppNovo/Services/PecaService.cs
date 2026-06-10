using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Helpers;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class PecaService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public PecaService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Peca>> ListarAsync()
    {
        const string sql = @"
            SELECT
                idpeca AS IdPeca,
                codigo AS Codigo,
                nome AS Nome,
                descricao AS Descricao,
                unidade AS Unidade,
                estoque_atual AS EstoqueAtual,
                estoque_minimo AS EstoqueMinimo,
                valor_custo AS ValorCusto,
                valor_venda AS ValorVenda
            FROM peca
            ORDER BY nome;";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Peca>(sql);
    }

    public async Task<int> InserirAsync(Peca peca)
    {
        Validar(peca);

        const string sql = @"
            INSERT INTO peca (codigo, nome, descricao, unidade, estoque_atual, estoque_minimo, valor_custo, valor_venda)
            VALUES (@Codigo, @Nome, @Descricao, @Unidade, @EstoqueAtual, @EstoqueMinimo, @ValorCusto, @ValorVenda);
            SELECT LAST_INSERT_ID();";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, peca);
    }

    public async Task AtualizarAsync(Peca peca)
    {
        Validar(peca);

        const string sql = @"
            UPDATE peca SET
                codigo = @Codigo,
                nome = @Nome,
                descricao = @Descricao,
                unidade = @Unidade,
                estoque_atual = @EstoqueAtual,
                estoque_minimo = @EstoqueMinimo,
                valor_custo = @ValorCusto,
                valor_venda = @ValorVenda
            WHERE idpeca = @IdPeca;";

        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(sql, peca);
    }

    private static void Validar(Peca peca)
    {
        ValidationHelper.ValidarTextoObrigatorio(peca.Nome, "nome");
        ValidationHelper.ValidarValorNaoNegativo(peca.ValorCusto, "valor de custo");
        ValidationHelper.ValidarValorNaoNegativo(peca.ValorVenda, "valor de venda");
        if (peca.EstoqueAtual < 0) throw new ArgumentException("Estoque atual não pode ser negativo.");
        if (peca.EstoqueMinimo < 0) throw new ArgumentException("Estoque mínimo não pode ser negativo.");
    }
}
