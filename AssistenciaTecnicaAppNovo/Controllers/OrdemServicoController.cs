using AssistenciaTecnicaAppNovo.Models;
using AssistenciaTecnicaAppNovo.Services;

namespace AssistenciaTecnicaAppNovo.Controllers;

public class OrdemServicoController
{
    private readonly OrdemServicoService _ordemServicoService;
    private readonly EstoqueService _estoqueService;

    public OrdemServicoController(OrdemServicoService ordemServicoService, EstoqueService estoqueService)
    {
        _ordemServicoService = ordemServicoService;
        _estoqueService = estoqueService;
    }

    public Task<IEnumerable<OrdemServico>> ListarAsync() => _ordemServicoService.ListarAsync();
    public Task<int> AbrirAsync(OrdemServico ordem) => _ordemServicoService.AbrirAsync(ordem);
    public Task AtualizarStatusAsync(int ordemServicoId, int statusId, DateTime? dataFechamento = null)
        => _ordemServicoService.AtualizarStatusAsync(ordemServicoId, statusId, dataFechamento);

    public async Task AdicionarPecaNaOrdemAsync(int ordemServicoId, int pecaId, int quantidade, decimal valorUnitario, int? tecnicoId)
    {
        await _ordemServicoService.AdicionarPecaAsync(ordemServicoId, pecaId, quantidade, valorUnitario);
        await _estoqueService.RegistrarSaidaPorOrdemAsync(pecaId, ordemServicoId, quantidade, valorUnitario, tecnicoId, "Saída por ordem de serviço");
    }
}
