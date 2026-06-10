namespace AssistenciaTecnicaAppNovo.Models;

public class StatusOrdemServico
{
    public int IdStatus { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Finalizado { get; set; }
}
