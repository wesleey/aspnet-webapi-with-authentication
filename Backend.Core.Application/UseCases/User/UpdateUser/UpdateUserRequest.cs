namespace Backend.Core.Application.UseCases.User.UpdateUser;

public sealed record UpdateUserRequest(string? FirstName, string? LastName, string? Email, string? Password);
