using AssistenciaTecnicaAppNovo.DTOs;
using AssistenciaTecnicaAppNovo.Services;

namespace AssistenciaTecnicaAppNovo.Controllers;

public class AuthController
{
    private readonly LoginService _loginService;

    public AuthController(LoginService loginService)
    {
        _loginService = loginService;
    }

    public Task<LoginResultado> LoginAsync(string email, string senha)
    {
        return _loginService.LoginAsync(email, senha);
    }
}
