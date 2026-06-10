namespace AssistenciaTecnicaAppNovo.Models;

public class Peca
{
    public int IdPeca { get; set; }
    public string? Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string? Unidade { get; set; } = "UN";
    public int EstoqueAtual { get; set; }
    public int EstoqueMinimo { get; set; }
    public decimal ValorCusto { get; set; }
    public decimal ValorVenda { get; set; }
}
