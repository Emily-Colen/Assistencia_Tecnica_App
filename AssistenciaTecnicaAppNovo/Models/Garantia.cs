namespace AssistenciaTecnicaAppNovo.Models;

public class Garantia
{
    public int IdGarantia { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Termo { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public int OrdemServicoIdOrdemServico { get; set; }
}
