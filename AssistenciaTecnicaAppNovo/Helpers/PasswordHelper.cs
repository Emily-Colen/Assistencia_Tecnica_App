using System.Security.Cryptography;
using System.Text;

namespace AssistenciaTecnicaAppNovo.Helpers;

public static class PasswordHelper
{
    // Para trabalho acadêmico. Em produção, use BCrypt/Argon2 com salt.
    public static string GerarHashSha256(string senha)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    public static bool VerificarSenha(string senhaDigitada, string senhaHashSalva)
    {
        var hashDigitado = GerarHashSha256(senhaDigitada);
        return hashDigitado == senhaHashSalva;
    }
}
