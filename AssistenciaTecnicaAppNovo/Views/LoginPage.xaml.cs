using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using AssistenciaTecnicaAppNovo.Controllers;

namespace AssistenciaTecnicaAppNovo.Views;

public partial class LoginPage : ContentPage
{
    private readonly AuthController _authController;
    private readonly IServiceProvider _serviceProvider;

    public LoginPage(AuthController authController, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _authController = authController;
        _serviceProvider = serviceProvider;
    }

    private async void OnEntrarClicked(object? sender, EventArgs e)
    {
        try
        {
            MensagemLabel.Text = string.Empty;

            var resultado = await _authController.LoginAsync(EmailEntry.Text ?? string.Empty, SenhaEntry.Text ?? string.Empty);

            if (!resultado.Sucesso)
            {
                MensagemLabel.Text = resultado.Mensagem;
                return;
            }

            var menu = _serviceProvider.GetRequiredService<MainMenuPage>();
            await Navigation.PushAsync(menu);
        }
        catch (Exception ex)
        {
            MensagemLabel.Text = ex.Message;
        }
    }
}
