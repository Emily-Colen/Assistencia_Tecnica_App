namespace AssistenciaTecnicaAppNovo.Models;

public class PecaOrdemServico
{
    public int PecaIdPeca { get; set; }
    public int OrdemServicoIdOrdemServico { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal { get; set; }
    public decimal ValorUnitario { get; set; }
}
