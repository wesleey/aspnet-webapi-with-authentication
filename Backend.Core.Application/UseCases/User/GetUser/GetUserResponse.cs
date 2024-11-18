namespace Backend.Core.Application.UseCases.User.GetUser;

public sealed record GetUserResponse
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
