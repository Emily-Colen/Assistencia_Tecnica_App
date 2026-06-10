using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Helpers;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class ClienteService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public ClienteService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Cliente>> ListarAsync()
    {
        const string sql = @"
            SELECT
                idcliente AS IdCliente,
                nome AS Nome,
                cpf_cnpj AS CpfCnpj,
                email AS Email,
                telefone AS Telefone,
                rua_idrua AS RuaIdRua,
                numero_endereco AS NumeroEndereco,
                complemento AS Complemento
            FROM cliente
            ORDER BY nome;";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Cliente>(sql);
    }

    public async Task<int> InserirAsync(Cliente cliente)
    {
        Validar(cliente);

        const string sql = @"
            INSERT INTO cliente (nome, cpf_cnpj, email, telefone, rua_idrua, numero_endereco, complemento)
            VALUES (@Nome, @CpfCnpj, @Email, @Telefone, @RuaIdRua, @NumeroEndereco, @Complemento);
            SELECT LAST_INSERT_ID();";

        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, cliente);
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        Validar(cliente);

        const string sql = @"
            UPDATE cliente SET
                nome = @Nome,
                cpf_cnpj = @CpfCnpj,
                email = @Email,
                telefone = @Telefone,
                rua_idrua = @RuaIdRua,
                numero_endereco = @NumeroEndereco,
                complemento = @Complemento
            WHERE idcliente = @IdCliente;";

        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(sql, cliente);
    }

    public async Task ExcluirAsync(int idCliente)
    {
        const string sql = "DELETE FROM cliente WHERE idcliente = @IdCliente;";
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(sql, new { IdCliente = idCliente });
    }

    private static void Validar(Cliente cliente)
    {
        ValidationHelper.ValidarTextoObrigatorio(cliente.Nome, "nome");
        ValidationHelper.ValidarTextoObrigatorio(cliente.CpfCnpj, "cpf/cnpj");
        ValidationHelper.ValidarTextoObrigatorio(cliente.Email, "email");
        ValidationHelper.ValidarTextoObrigatorio(cliente.Telefone, "telefone");

        if (cliente.RuaIdRua <= 0)
            throw new ArgumentException("Selecione uma rua válida.");
    }
}
