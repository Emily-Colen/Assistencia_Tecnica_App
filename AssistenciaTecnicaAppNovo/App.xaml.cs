using AssistenciaTecnicaAppNovo.Views;

namespace AssistenciaTecnicaAppNovo;

public partial class App : Application
{
    public App(LoginPage loginPage)
    {
        InitializeComponent();

        MainPage = new NavigationPage(loginPage);
    }
}
