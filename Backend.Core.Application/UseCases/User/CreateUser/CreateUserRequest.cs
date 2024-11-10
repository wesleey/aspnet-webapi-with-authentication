namespace Backend.Core.Application.UseCases.User.CreateUser;

public sealed record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
