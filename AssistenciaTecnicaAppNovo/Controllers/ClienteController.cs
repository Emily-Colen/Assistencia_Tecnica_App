using AssistenciaTecnicaAppNovo.Models;
using AssistenciaTecnicaAppNovo.Services;

namespace AssistenciaTecnicaAppNovo.Controllers;

public class ClienteController
{
    private readonly ClienteService _clienteService;

    public ClienteController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public Task<IEnumerable<Cliente>> ListarAsync() => _clienteService.ListarAsync();
    public Task<int> InserirAsync(Cliente cliente) => _clienteService.InserirAsync(cliente);
    public Task AtualizarAsync(Cliente cliente) => _clienteService.AtualizarAsync(cliente);
    public Task ExcluirAsync(int idCliente) => _clienteService.ExcluirAsync(idCliente);
}
