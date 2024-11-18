using Backend.Core.Domain.Enums;

namespace Backend.Core.Application.UseCases.User.CreateUser;

public sealed record CreateUserResponse
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
