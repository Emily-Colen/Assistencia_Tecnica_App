using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.DTOs;
using AssistenciaTecnicaAppNovo.Helpers;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;

namespace AssistenciaTecnicaAppNovo.Services;

public class LoginService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public LoginService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<LoginResultado> LoginAsync(string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            return new LoginResultado { Sucesso = false, Mensagem = "Informe e-mail e senha." };
        }

        using var conn = _connectionFactory.CreateConnection();

        const string sql = @"
            SELECT
                idtecnico AS IdTecnico,
                nome AS Nome,
                email AS Email,
                senha_hash AS SenhaHash,
                ativo AS Ativo
            FROM tecnico
            WHERE email = @Email
            LIMIT 1;";

        var tecnico = await conn.QueryFirstOrDefaultAsync<Tecnico>(sql, new { Email = email });

        if (tecnico == null)
            return new LoginResultado { Sucesso = false, Mensagem = "Usuário não encontrado." };

        if (!tecnico.Ativo)
            return new LoginResultado { Sucesso = false, Mensagem = "Usuário inativo." };

        if (!PasswordHelper.VerificarSenha(senha, tecnico.SenhaHash))
            return new LoginResultado { Sucesso = false, Mensagem = "Senha inválida." };

        return new LoginResultado { Sucesso = true, Mensagem = "Login realizado com sucesso.", Tecnico = tecnico };
    }
}
