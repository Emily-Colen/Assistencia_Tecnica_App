namespace AssistenciaTecnicaAppNovo.Models;

public class Bairro
{
    public int IdBairro { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CidadeIdCidade { get; set; }
}
