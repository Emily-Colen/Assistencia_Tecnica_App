using AssistenciaTecnicaAppNovo.Models;

namespace AssistenciaTecnicaAppNovo.DTOs;

public class LoginResultado
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public Tecnico? Tecnico { get; set; }
}
