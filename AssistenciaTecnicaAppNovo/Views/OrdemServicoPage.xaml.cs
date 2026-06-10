using Microsoft.Maui.Controls;
using AssistenciaTecnicaAppNovo.Controllers;
using AssistenciaTecnicaAppNovo.Models;
using System.Globalization;

namespace AssistenciaTecnicaAppNovo.Views;

public partial class OrdemServicoPage : ContentPage
{
    private readonly OrdemServicoController _ordemServicoController;

    public OrdemServicoPage(OrdemServicoController ordemServicoController)
    {
        InitializeComponent();
        _ordemServicoController = ordemServicoController;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarOrdensAsync();
    }

    private async Task CarregarOrdensAsync()
    {
        OrdensCollection.ItemsSource = await _ordemServicoController.ListarAsync();
    }

    private async void OnAbrirClicked(object? sender, EventArgs e)
    {
        try
        {
            var ordem = new OrdemServico
            {
                EquipamentoIdEquipamento = int.TryParse(EquipamentoIdEntry.Text, out var equipamentoId) ? equipamentoId : 0,
                TecnicoIdTecnico = int.TryParse(TecnicoIdEntry.Text, out var tecnicoId) ? tecnicoId : null,
                StatusIdStatus = int.TryParse(StatusIdEntry.Text, out var statusId) ? statusId : 0,
                DefeitoRelatado = DefeitoEditor.Text ?? string.Empty,
                DescricaoManutencao = DescricaoEditor.Text,
                Preco = decimal.TryParse(PrecoEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var preco) ? preco : 0
            };

            int id = await _ordemServicoController.AbrirAsync(ordem);
            await DisplayAlertAsync("Sucesso", $"Ordem de serviço #{id} aberta.", "OK");
            await CarregarOrdensAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private async void OnAtualizarListaClicked(object? sender, EventArgs e)
    {
        await CarregarOrdensAsync();
    }
}
