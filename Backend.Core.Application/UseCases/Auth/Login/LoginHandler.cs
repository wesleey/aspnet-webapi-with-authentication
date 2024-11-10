using Backend.Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Application.UseCases.Auth.Login;

public class LoginHandler(LoginUseCase loginUseCase)
{
    private readonly LoginUseCase _loginUseCase = loginUseCase;

    public async Task<IResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Email) || !Email.IsValidEmailAddress(request.Email))
                throw new Exception("Invalid email");

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
                throw new Exception("Invalid password");

            var (token, user) = await _loginUseCase.Execute(request.Email, request.Password, cancellationToken);

            var data = new LoginResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return Results.Ok(new { success = true, message = "User logged in successfully", token, data });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { success = false, message = ex.Message });
        }
    }
}
