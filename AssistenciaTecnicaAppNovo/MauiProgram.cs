using AssistenciaTecnicaAppNovo.Controllers;
using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.Services;
using AssistenciaTecnicaAppNovo.Views;
using Microsoft.Extensions.Logging;

namespace AssistenciaTecnicaAppNovo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Database
        builder.Services.AddSingleton<DatabaseConfig>();
        builder.Services.AddSingleton<MySqlConnectionFactory>();
        builder.Services.AddSingleton<SQLiteConnectionFactory>();
        builder.Services.AddSingleton<MongoConnectionFactory>();

        // Services
        builder.Services.AddTransient<LoginService>();
        builder.Services.AddTransient<ClienteService>();
        builder.Services.AddTransient<TecnicoService>();
        builder.Services.AddTransient<EquipamentoService>();
        builder.Services.AddTransient<StatusOrdemServicoService>();
        builder.Services.AddTransient<OrdemServicoService>();
        builder.Services.AddTransient<PecaService>();
        builder.Services.AddTransient<EstoqueService>();
        builder.Services.AddTransient<JsonBackupService>();
        builder.Services.AddTransient<MongoAnexoService>();
        builder.Services.AddTransient<LocalCacheService>();

        // Controllers
        builder.Services.AddTransient<AuthController>();
        builder.Services.AddTransient<ClienteController>();
        builder.Services.AddTransient<OrdemServicoController>();
        builder.Services.AddTransient<PecaController>();

        // Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<ClientePage>();
        builder.Services.AddTransient<PecaPage>();
        builder.Services.AddTransient<OrdemServicoPage>();

        return builder.Build();
    }
}
