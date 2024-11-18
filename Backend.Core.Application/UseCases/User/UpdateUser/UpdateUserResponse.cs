namespace Backend.Core.Application.UseCases.User.UpdateUser;

public sealed record UpdateUserResponse
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
