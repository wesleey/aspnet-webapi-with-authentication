using Backend.Core.Application.UseCases.Auth.Login;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(LoginHandler loginHandler) : ControllerBase
{
    private readonly LoginHandler _loginHandler = loginHandler;

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        => await _loginHandler.Handle(request, cancellationToken);
}
