namespace Backend.Core.Application.UseCases.User.CreateUser;

public sealed record CreateUserResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
