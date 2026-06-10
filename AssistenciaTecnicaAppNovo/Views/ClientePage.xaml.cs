using Microsoft.Maui.Controls;
using AssistenciaTecnicaAppNovo.Controllers;
using AssistenciaTecnicaAppNovo.Models;

namespace AssistenciaTecnicaAppNovo.Views;

public partial class ClientePage : ContentPage
{
    private readonly ClienteController _clienteController;

    public ClientePage(ClienteController clienteController)
    {
        InitializeComponent();
        _clienteController = clienteController;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarClientesAsync();
    }

    private async Task CarregarClientesAsync()
    {
        ClientesCollection.ItemsSource = await _clienteController.ListarAsync();
    }

    private async void OnSalvarClicked(object? sender, EventArgs e)
    {
        try
        {
            var cliente = new Cliente
            {
                Nome = NomeEntry.Text ?? string.Empty,
                CpfCnpj = CpfCnpjEntry.Text ?? string.Empty,
                Email = EmailEntry.Text ?? string.Empty,
                Telefone = TelefoneEntry.Text ?? string.Empty,
                RuaIdRua = int.TryParse(RuaIdEntry.Text, out var ruaId) ? ruaId : 0,
                NumeroEndereco = NumeroEntry.Text,
                Complemento = ComplementoEntry.Text
            };

            await _clienteController.InserirAsync(cliente);
            await DisplayAlertAsync("Sucesso", "Cliente salvo.", "OK");
            await CarregarClientesAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private async void OnAtualizarListaClicked(object? sender, EventArgs e)
    {
        await CarregarClientesAsync();
    }
}
