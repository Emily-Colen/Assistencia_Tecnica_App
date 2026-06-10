using AssistenciaTecnicaAppNovo.Models;
using AssistenciaTecnicaAppNovo.Services;

namespace AssistenciaTecnicaAppNovo.Controllers;

public class PecaController
{
    private readonly PecaService _pecaService;
    private readonly EstoqueService _estoqueService;

    public PecaController(PecaService pecaService, EstoqueService estoqueService)
    {
        _pecaService = pecaService;
        _estoqueService = estoqueService;
    }

    public Task<IEnumerable<Peca>> ListarAsync() => _pecaService.ListarAsync();
    public Task<int> InserirAsync(Peca peca) => _pecaService.InserirAsync(peca);
    public Task AtualizarAsync(Peca peca) => _pecaService.AtualizarAsync(peca);

    public Task RegistrarEntradaAsync(int pecaId, int quantidade, decimal valorUnitario, int? tecnicoId, string? observacao)
        => _estoqueService.RegistrarEntradaAsync(pecaId, quantidade, valorUnitario, tecnicoId, observacao);
}
