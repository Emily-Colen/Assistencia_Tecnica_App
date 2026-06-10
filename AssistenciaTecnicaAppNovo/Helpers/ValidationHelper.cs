namespace AssistenciaTecnicaAppNovo.Helpers;

public static class ValidationHelper
{
    public static void ValidarTextoObrigatorio(string valor, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"O campo {nomeCampo} é obrigatório.");
    }

    public static void ValidarValorNaoNegativo(decimal valor, string nomeCampo)
    {
        if (valor < 0)
            throw new ArgumentException($"O campo {nomeCampo} não pode ser negativo.");
    }

    public static void ValidarQuantidadePositiva(int quantidade, string nomeCampo = "quantidade")
    {
        if (quantidade <= 0)
            throw new ArgumentException($"O campo {nomeCampo} deve ser maior que zero.");
    }
}
