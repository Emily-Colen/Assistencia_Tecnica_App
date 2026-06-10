namespace AssistenciaTecnicaAppNovo.Models;

public class MovimentacaoEstoque
{
    public int IdMovimentacaoEstoque { get; set; }
    public string TipoMovimentacao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public string? Observacao { get; set; }
    public int PecaIdPeca { get; set; }
    public int? OrdemServicoIdOrdemServico { get; set; }
    public int? TecnicoIdTecnico { get; set; }
}
