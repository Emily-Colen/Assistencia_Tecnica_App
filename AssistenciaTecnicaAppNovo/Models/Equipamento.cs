namespace AssistenciaTecnicaAppNovo.Models;

public class Equipamento
{
    public int IdEquipamento { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int ClienteIdCliente { get; set; }
}
