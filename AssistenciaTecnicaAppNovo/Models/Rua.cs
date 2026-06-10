namespace AssistenciaTecnicaAppNovo.Models;

public class Rua
{
    public int IdRua { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int BairroIdBairro { get; set; }
}
