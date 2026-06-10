namespace AssistenciaTecnicaAppNovo.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int RuaIdRua { get; set; }
    public string? NumeroEndereco { get; set; }
    public string? Complemento { get; set; }
}
