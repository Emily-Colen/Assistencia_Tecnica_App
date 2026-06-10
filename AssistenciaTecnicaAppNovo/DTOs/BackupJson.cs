using AssistenciaTecnicaAppNovo.Models;

namespace AssistenciaTecnicaAppNovo.DTOs;

public class BackupJson
{
    public List<Estado> Estados { get; set; } = new();
    public List<Cidade> Cidades { get; set; } = new();
    public List<Bairro> Bairros { get; set; } = new();
    public List<Rua> Ruas { get; set; } = new();
    public List<Cliente> Clientes { get; set; } = new();
    public List<Tecnico> Tecnicos { get; set; } = new();
    public List<Especialidade> Especialidades { get; set; } = new();
    public List<TecnicoEspecialidade> TecnicoEspecialidades { get; set; } = new();
    public List<Equipamento> Equipamentos { get; set; } = new();
    public List<StatusOrdemServico> StatusOrdensServico { get; set; } = new();
    public List<OrdemServico> OrdensServico { get; set; } = new();
    public List<Peca> Pecas { get; set; } = new();
    public List<PecaOrdemServico> PecasOrdensServico { get; set; } = new();
    public List<MovimentacaoEstoque> MovimentacoesEstoque { get; set; } = new();
    public List<Garantia> Garantias { get; set; } = new();
}
