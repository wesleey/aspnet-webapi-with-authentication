namespace Backend.Core.Application.UseCases.Auth.Login;

public sealed record LoginRequest(string Email, string Password);
