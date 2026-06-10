using Microsoft.Maui.Controls;
using AssistenciaTecnicaAppNovo.Controllers;
using AssistenciaTecnicaAppNovo.Models;
using System.Globalization;

namespace AssistenciaTecnicaAppNovo.Views;

public partial class PecaPage : ContentPage
{
    private readonly PecaController _pecaController;

    public PecaPage(PecaController pecaController)
    {
        InitializeComponent();
        _pecaController = pecaController;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarPecasAsync();
    }

    private async Task CarregarPecasAsync()
    {
        PecasCollection.ItemsSource = await _pecaController.ListarAsync();
    }

    private async void OnSalvarClicked(object? sender, EventArgs e)
    {
        try
        {
            var peca = new Peca
            {
                Codigo = CodigoEntry.Text,
                Nome = NomeEntry.Text ?? string.Empty,
                Descricao = DescricaoEntry.Text ?? string.Empty,
                Unidade = "UN",
                EstoqueAtual = int.TryParse(EstoqueEntry.Text, out var estoque) ? estoque : 0,
                EstoqueMinimo = int.TryParse(EstoqueMinimoEntry.Text, out var estoqueMinimo) ? estoqueMinimo : 0,
                ValorCusto = decimal.TryParse(ValorCustoEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var custo) ? custo : 0,
                ValorVenda = decimal.TryParse(ValorVendaEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var venda) ? venda : 0
            };

            await _pecaController.InserirAsync(peca);
            await DisplayAlertAsync("Sucesso", "Peça salva.", "OK");
            await CarregarPecasAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private async void OnAtualizarListaClicked(object? sender, EventArgs e)
    {
        await CarregarPecasAsync();
    }
}
