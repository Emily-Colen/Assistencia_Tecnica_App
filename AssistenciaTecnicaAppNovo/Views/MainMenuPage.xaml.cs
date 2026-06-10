using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Extensions.DependencyInjection;
using AssistenciaTecnicaAppNovo.Services;

namespace AssistenciaTecnicaAppNovo.Views;

public partial class MainMenuPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly JsonBackupService _jsonBackupService;

    public MainMenuPage(IServiceProvider serviceProvider, JsonBackupService jsonBackupService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _jsonBackupService = jsonBackupService;
    }

    private async void OnClientesClicked(object? sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(_serviceProvider.GetRequiredService<ClientePage>());
    }

    private async void OnPecasClicked(object? sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(_serviceProvider.GetRequiredService<PecaPage>());
    }

    private async void OnOrdensClicked(object? sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(_serviceProvider.GetRequiredService<OrdemServicoPage>());
    }

    private async void OnExportarJsonClicked(object? sender, TappedEventArgs e)
    {
        try
        {
            string caminho = Path.Combine(FileSystem.AppDataDirectory, $"backup_assistencia_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            await _jsonBackupService.ExportarAsync(caminho);
            await DisplayAlertAsync("Sucesso", $"Backup exportado em:\n{caminho}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private async void OnImportarJsonClicked(object? sender, TappedEventArgs e)
    {
        try
        {
            var resultado = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Selecione o arquivo JSON"
            });

            if (resultado == null)
                return;

            await _jsonBackupService.LerArquivoAsync(resultado.FullPath);
            await DisplayAlertAsync("Arquivo válido", "O JSON foi lido e validado. A gravação no banco pode ser implementada conforme a regra do professor.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }
}
