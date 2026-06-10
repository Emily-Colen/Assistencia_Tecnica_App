namespace AssistenciaTecnicaAppNovo.Models;

public class OrdemServico
{
    public int IdOrdemServico { get; set; }
    public string DefeitoRelatado { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }
    public DateTime? DataPrevista { get; set; }
    public string? DescricaoManutencao { get; set; }
    public int StatusIdStatus { get; set; }
    public int? TecnicoIdTecnico { get; set; }
    public int EquipamentoIdEquipamento { get; set; }
}
